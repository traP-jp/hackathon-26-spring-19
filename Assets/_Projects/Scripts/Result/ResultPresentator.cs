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
    private readonly SoundPlayer soundPlayer;
    private readonly CompositeDisposable disposables = new();

    public ResultPresentator(
        ResultInfo resultInfo,
        ResultViewer resultViewer,
        GameSessionData gameSessionData,
        SoundPlayer soundPlayer)
    {
        this.resultInfo = resultInfo;
        this.resultViewer = resultViewer;
        this.gameSessionData = gameSessionData;
        this.soundPlayer = soundPlayer;
    }

    public void Start()
    {
        resultInfo.OnRetryClicked
            .Subscribe(_ => OnRetryClicked())
            .AddTo(disposables);

        resultInfo.OnTitleClicked
            .Subscribe(_ => OnTitleClicked())
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

    //リトライボタンを処理
    private void OnRetryClicked()
    {
        soundPlayer.PlayButton();
        Retry();
    }

    //タイトルボタンを処理
    private void OnTitleClicked()
    {
        soundPlayer.PlayButton();
        SceneManager.LoadScene(TitleSceneName);
    }
}
