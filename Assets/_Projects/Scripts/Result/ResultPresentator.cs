using System;
using R3;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public sealed class ResultPresentator : IStartable, IDisposable
{
    private const string GameSceneName = "GameScene_moti";
    private const string TitleSceneName = "TitleScene";

    private readonly ResultInfo resultInfo;
    private readonly ResultViewer resultViewer;
    private readonly GameSessionData gameSessionData;
    private readonly CompositeDisposable disposables = new();

    public ResultPresentator(
        ResultInfo resultInfo,
        ResultViewer resultViewer,
        GameSessionData gameSessionData)
    {
        this.resultInfo = resultInfo;
        this.resultViewer = resultViewer;
        this.gameSessionData = gameSessionData;
    }

    public void Start()
    {
        resultInfo.OnRetryClicked
            .Subscribe(_ => Retry())
            .AddTo(disposables);

        resultInfo.OnTitleClicked
            .Subscribe(_ => SceneManager.LoadScene(TitleSceneName))
            .AddTo(disposables);

        if (gameSessionData.lastResult == null)
        {
            UnityEngine.Debug.LogWarning(
                "ResultData が保存されていません。ゲーム終了時に GameSessionData.SaveResult を呼んでください。"
            );
        }
        resultViewer.Show(gameSessionData.lastResult);
    }

    public void Dispose()
    {
        disposables.Dispose();
    }

    private void Retry()
    {
        gameSessionData.ClearResult();
        SceneManager.LoadScene(GameSceneName);
    }
}
