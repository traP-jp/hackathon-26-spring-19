using R3;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 一時停止関連の UI 入力を Observable として公開する。
/// ゲームの停止や Scene 遷移は行わない。
/// </summary>
public sealed class PauseInfo : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button titleButton;

    public Observable<Unit> OnPauseClicked { get; private set; }
    public Observable<Unit> OnResumeClicked { get; private set; }
    public Observable<Unit> OnTitleClicked { get; private set; }

    private void Awake()
    {
        OnPauseClicked = pauseButton != null ? pauseButton.OnClickAsObservable() : Observable.Empty<Unit>();
        OnResumeClicked = resumeButton != null ? resumeButton.OnClickAsObservable() : Observable.Empty<Unit>();
        OnTitleClicked = titleButton != null ? titleButton.OnClickAsObservable() : Observable.Empty<Unit>();
    }

    public void SetPauseButtonInteractable(bool interactable)
    {
        if (pauseButton != null) pauseButton.interactable = interactable;
    }

    public void SetMenuButtonsInteractable(bool interactable)
    {
        if (resumeButton != null) resumeButton.interactable = interactable;
        if (titleButton != null) titleButton.interactable = interactable;
    }
}
