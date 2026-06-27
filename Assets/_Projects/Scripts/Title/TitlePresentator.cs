using System;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public sealed class TitlePresentator : IStartable, IDisposable
{
    private const string GameSceneName = "GameScene";

    private readonly TitleInfo titleInfo;
    private readonly TitleViewer titleViewer;
    private readonly GameSessionData gameSessionData;
    private readonly DifficultyParam[] difficultyParams;
    private readonly CompositeDisposable disposables = new();

    private DifficultyType selectedDifficulty;

    public TitlePresentator(
        TitleInfo titleInfo,
        TitleViewer titleViewer,
        GameSessionData gameSessionData,
        DifficultyParam[] difficultyParams)
    {
        this.titleInfo = titleInfo;
        this.titleViewer = titleViewer;
        this.gameSessionData = gameSessionData;
        this.difficultyParams = difficultyParams;
    }

    public void Start()
    {
        titleInfo.OnDifficultyChanged
            .Subscribe(SetDifficulty)
            .AddTo(disposables);

        titleInfo.OnStartClicked
            .Subscribe(_ => StartGame())
            .AddTo(disposables);

        titleInfo.OnExitClicked
            .Subscribe(_ => ExitGame())
            .AddTo(disposables);

        SetDifficulty(titleInfo.GetSelectedDifficulty());
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
