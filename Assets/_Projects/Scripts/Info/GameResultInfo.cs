using R3;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ゲーム終了オーバーレイのボタン入力を公開する。</summary>
public sealed class GameResultInfo : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button titleButton;

    public Observable<Unit> OnRestartClicked { get; private set; }
    public Observable<Unit> OnTitleClicked { get; private set; }

    private void Awake()
    {
        OnRestartClicked = restartButton.OnClickAsObservable();
        OnTitleClicked = titleButton.OnClickAsObservable();
    }
}
