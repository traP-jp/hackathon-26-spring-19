using System;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public sealed class TitlePresentator : IStartable, IDisposable
{
    private const string GameSceneName = "GameScene_moti";

    private readonly TitleInfo titleInfo;
    private readonly TitleViewer titleViewer;
    private readonly GameSessionData gameSessionData;
    private readonly DifficultyParam[] difficultyParams;
    private readonly SoundPlayer soundPlayer;
    private readonly CompositeDisposable disposables = new();

    private DifficultyType selectedDifficulty;

    public TitlePresentator(
        TitleInfo titleInfo,
        TitleViewer titleViewer,
        GameSessionData gameSessionData,
        DifficultyParam[] difficultyParams,
        SoundPlayer soundPlayer)
    {
        this.titleInfo = titleInfo;
        this.titleViewer = titleViewer;
        this.gameSessionData = gameSessionData;
        this.difficultyParams = difficultyParams;
        this.soundPlayer = soundPlayer;
    }

    public void Start()
    {
        titleInfo.OnDifficultyChanged
            .Subscribe(OnDifficultyChanged)
            .AddTo(disposables);

        titleInfo.OnStartClicked
            .Subscribe(_ => OnStartClicked())
            .AddTo(disposables);

        SetDifficulty(titleInfo.GetSelectedDifficulty());
    }

    //難易度選択を処理
    private void OnDifficultyChanged(DifficultyType difficultyType)
    {
        soundPlayer.PlayButton();
        SetDifficulty(difficultyType);
    }

    //スタートボタンを処理
    private void OnStartClicked()
    {
        soundPlayer.PlayButton();
        StartGame();
    }

    //終了ボタンを処理
    private void OnExitClicked()
    {
        soundPlayer.PlayButton();
        ExitGame();
    }

    private void SetDifficulty(DifficultyType difficultyType)
    {
        selectedDifficulty = difficultyType;
        titleViewer.SetDifficulty(FindDifficulty(difficultyType));
    }

    private DifficultyParam FindDifficulty(DifficultyType difficultyType)
    {
        if (difficultyParams == null)
        {
            return null;
        }

        foreach (DifficultyParam difficulty in difficultyParams)
        {
            if (difficulty != null && difficulty.difficultyType == difficultyType)
            {
                return difficulty;
            }
        }

        return null;
    }

    private void StartGame()
    {
        gameSessionData.selectedDifficulty = selectedDifficulty;
        gameSessionData.ClearResult();
        SceneManager.LoadScene(GameSceneName);
    }

    private static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Dispose()
    {
        disposables.Dispose();
    }
}
