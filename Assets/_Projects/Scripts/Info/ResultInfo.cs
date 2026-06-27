using R3;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルト画面の UI 入力を Observable として公開する。
/// Scene 遷移は行わない。
/// </summary>
public sealed class ResultInfo : MonoBehaviour
{
    [SerializeField]
    private Button retryButton;

    [SerializeField]
    private Button titleButton;

    public Observable<Unit> OnRetryClicked { get; private set; }
    public Observable<Unit> OnTitleClicked { get; private set; }

    private void Awake()
    {
        OnRetryClicked = retryButton.OnClickAsObservable();
        OnTitleClicked = titleButton.OnClickAsObservable();
    }

    public void SetInteractable(bool interactable)
    {
        retryButton.interactable = interactable;
        titleButton.interactable = interactable;
    }
}
