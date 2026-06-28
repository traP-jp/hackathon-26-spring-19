using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public sealed class GameMainPresentator : IStartable, ITickable, IDisposable
{
    private const string GameSceneName = "GameScene_moti";
    private const string TitleSceneName = "TitleScene";
    //初期ライフを固定値で持つため
    private const int InitialLife = 3;

    private readonly GameData gameData;
    private readonly GameSessionData gameSessionData;
    private readonly IReadOnlyList<DifficultyParam> difficulties;
    private readonly ItemSpawner itemSpawner;
    private readonly PlayerController playerController;
    private readonly GameUIViewer gameUIViewer;
    private readonly PauseInfo pauseInfo;
    private readonly GameResultInfo gameResultInfo;
    private readonly GameResultViewer gameResultViewer;
    private readonly TimerSystem timerSystem;
    private readonly ItemEffectSystem itemEffectSystem;
    private readonly GameJudgeSystem gameJudgeSystem;
    private readonly CompositeDisposable disposables = new();

    private bool isFinished;

    //依存を受け取る
    public GameMainPresentator(
        GameData gameData,
        GameSessionData gameSessionData,
        DifficultyParam[] difficulties,
        ItemSpawner itemSpawner,
        PlayerController playerController,
        GameUIViewer gameUIViewer,
        PauseInfo pauseInfo,
        GameResultInfo gameResultInfo,
        GameResultViewer gameResultViewer,
        TimerSystem timerSystem,
        ItemEffectSystem itemEffectSystem,
        GameJudgeSystem gameJudgeSystem)
    {
        this.gameData = gameData;
        this.gameSessionData = gameSessionData;
        this.difficulties = difficulties;
        this.itemSpawner = itemSpawner;
        this.playerController = playerController;
        this.gameUIViewer = gameUIViewer;
        this.pauseInfo = pauseInfo;
        this.gameResultInfo = gameResultInfo;
        this.gameResultViewer = gameResultViewer;
        this.timerSystem = timerSystem;
        this.itemEffectSystem = itemEffectSystem;
        this.gameJudgeSystem = gameJudgeSystem;
    }

    //本編を開始
    public void Start()
    {
        InitializeGameData();
        InitializeComponents();
        BindEvents();
        gameResultViewer.Hide();
        RefreshUI();

        gameData.phase = GamePhase.Playing;
        itemSpawner.StartSpawn();
        playerController.SetCanmove(true);
        pauseInfo.SetPauseButtonInteractable(true);
        pauseInfo.SetMenuButtonsInteractable(false);
    }

    //毎フレーム更新
    public void Tick()
    {
        if (gameData.phase != GamePhase.Playing || isFinished)
        {
            return;
        }

        UpdatePlaying(Time.deltaTime);
    }

    //状態を初期化
    private void InitializeGameData()
    {
        DifficultyParam difficulty = FindDifficultyParam(gameSessionData.selectedDifficulty);

        gameData.phase = GamePhase.Ready;
        gameData.difficulty = difficulty;
        gameData.timeLimit = difficulty.timeLimit;
        gameData.maxLife = InitialLife;
        gameData.currentLife = InitialLife;
        gameData.score = 0;
        gameData.resultType = ResultType.GameOver;
        gameData.itemCountData = new ItemCountData();
        isFinished = false;
    }

    //難易度を探す
    private DifficultyParam FindDifficultyParam(DifficultyType difficultyType)
    {
        foreach (DifficultyParam difficulty in difficulties)
        {
            if (difficulty.difficultyType == difficultyType)
            {
                return difficulty;
            }
        }

        return difficulties[0];
    }

    //部品を初期化
    private void InitializeComponents()
    {
        timerSystem.Initialize(gameData);
        itemSpawner.Initialize(gameData.difficulty);
        playerController.SetCanmove(false);
        Time.timeScale = 1f;
    }

    //イベントを購読
    private void BindEvents()
    {
        itemSpawner.OnItemCollected
            .Subscribe(OnItemCollected)
            .AddTo(disposables);

        pauseInfo.OnPauseClicked
            .Subscribe(_ => PauseGame())
            .AddTo(disposables);

        pauseInfo.OnResumeClicked
            .Subscribe(_ => ResumeGame())
            .AddTo(disposables);

        pauseInfo.OnTitleClicked
            .Subscribe(_ => GoToTitleScene())
            .AddTo(disposables);

        gameResultInfo.OnRestartClicked
            .Subscribe(_ => RestartGame())
            .AddTo(disposables);

        gameResultInfo.OnTitleClicked
            .Subscribe(_ => GoToTitleScene())
            .AddTo(disposables);
    }

    //進行中を更新
    private void UpdatePlaying(float deltaTime)
    {
        timerSystem.UpdateTimer(gameData, deltaTime);
        CheckFinish();
        RefreshUI();
    }

    //取得効果を反映
    private void OnItemCollected(ItemParam itemParam)
    {
        if (isFinished)
        {
            return;
        }

        ItemEffectResult effectResult = itemEffectSystem.ApplyItemEffect(gameData, itemParam);

        ApplyItemEffectView(effectResult);
        CheckFinish();
        RefreshUI();
    }

    //効果表示を反映
    private void ApplyItemEffectView(ItemEffectResult effectResult)
    {
        if (effectResult.isDamage)
        {
            Debug.Log("Damage item collected.");
        }
        else if (effectResult.isLifeRecovered)
        {
            Debug.Log("Heal item collected.");
        }
        else if (effectResult.isScoreAdded)
        {
            Debug.Log("Score item collected.");
        }
    }

    //終了条件を確認
    private void CheckFinish()
    {
        if (isFinished || !gameJudgeSystem.IsFinished(gameData))
        {
            return;
        }

        FinishGame(gameJudgeSystem.JudgeResult(gameData));
    }

    //ゲームを終了
    private void FinishGame(ResultType resultType)
    {
        isFinished = true;
        itemSpawner.StopSpawn();
        playerController.SetCanmove(false);
        Time.timeScale = 1f;

        gameJudgeSystem.ApplyResult(gameData, resultType);
        gameSessionData.SaveResult(gameData);
        RefreshUI();
        pauseInfo.SetPauseButtonInteractable(false);
        pauseInfo.SetMenuButtonsInteractable(false);
        gameResultViewer.Show(resultType);
    }

    //結果データを作成
    private ResultData CreateResultData()
    {
        return new ResultData
        {
            resultType = gameData.resultType,
            difficultyType = gameData.difficulty.difficultyType,
            survivalTime = Mathf.Max(0f, gameData.elapsedTime),
            remainingLife = Mathf.Max(0, gameData.currentLife),
            finalScore = Mathf.Max(0, gameData.score),
            itemCountData = CopyItemCountData(gameData.itemCountData)
        };
    }

    //取得数を複製
    private ItemCountData CopyItemCountData(ItemCountData itemCountData)
    {
        return new ItemCountData
        {
            alcoholCount = Mathf.Max(0, itemCountData.alcoholCount),
            healCount = Mathf.Max(0, itemCountData.healCount),
            scoreItemCount = Mathf.Max(0, itemCountData.scoreItemCount)
        };
    }

    //リザルトへ遷移
    private void RestartGame()
    {
        Time.timeScale = 1f;
        gameSessionData.ClearResult();
        SceneManager.LoadScene(GameSceneName);
    }

    //一時停止する
    private void PauseGame()
    {
        if (gameData.phase != GamePhase.Playing || isFinished)
        {
            return;
        }

        gameData.phase = GamePhase.Paused;
        itemSpawner.StopSpawn();
        playerController.SetCanmove(false);
        pauseInfo.SetPauseButtonInteractable(false);
        pauseInfo.SetMenuButtonsInteractable(true);
        Time.timeScale = 0f;
    }

    //再開する
    private void ResumeGame()
    {
        if (gameData.phase != GamePhase.Paused || isFinished)
        {
            return;
        }

        Time.timeScale = 1f;
        gameData.phase = GamePhase.Playing;
        itemSpawner.StartSpawn();
        playerController.SetCanmove(true);
        pauseInfo.SetPauseButtonInteractable(true);
        pauseInfo.SetMenuButtonsInteractable(false);
    }

    //タイトルへ戻る
    private void GoToTitleScene()
    {
        Time.timeScale = 1f;
        itemSpawner.StopSpawn();
        playerController.SetCanmove(false);
        SceneManager.LoadScene(TitleSceneName);
    }

    //UIを更新
    private void RefreshUI()
    {
        gameUIViewer.Refresh(gameData);
    }

    //購読を破棄
    public void Dispose()
    {
        Time.timeScale = 1f;
        disposables.Dispose();
    }
}
