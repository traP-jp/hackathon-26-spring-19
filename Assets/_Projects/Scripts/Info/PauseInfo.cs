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
        OnPauseClicked = pauseButton.OnClickAsObservable();
        OnResumeClicked = resumeButton.OnClickAsObservable();
        OnTitleClicked = titleButton.OnClickAsObservable();
    }

    public void SetPauseButtonInteractable(bool interactable)
    {
        pauseButton.interactable = interactable;
    }

    public void SetMenuButtonsInteractable(bool interactable)
    {
        resumeButton.interactable = interactable;
        titleButton.interactable = interactable;
    }
}
