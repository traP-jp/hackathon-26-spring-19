using R3;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面の UI 入力を Observable として公開する。
/// Scene 遷移やゲーム状態の更新は行わない。
/// </summary>
public sealed class TitleInfo : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private Toggle easyToggle;

    [SerializeField]
    private Toggle normalToggle;

    [SerializeField]
    private Toggle hardToggle;

    private readonly Subject<DifficultyType> difficultyChangedSubject = new();
    private readonly CompositeDisposable disposables = new();

    public Observable<Unit> OnStartClicked { get; private set; }
    public Observable<Unit> OnExitClicked { get; private set; }
    public Observable<DifficultyType> OnDifficultyChanged => difficultyChangedSubject;

    private void Awake()
    {
        OnStartClicked = startButton.OnClickAsObservable();

        BindDifficultyToggle(easyToggle, DifficultyType.Easy);
        BindDifficultyToggle(normalToggle, DifficultyType.Normal);
        BindDifficultyToggle(hardToggle, DifficultyType.Hard);
    }

    private void BindDifficultyToggle(Toggle toggle, DifficultyType difficultyType)
    {
        toggle.OnValueChangedAsObservable()
            .Where(isOn => isOn)
            .Subscribe(_ => difficultyChangedSubject.OnNext(difficultyType))
            .AddTo(disposables);
    }

    public DifficultyType GetSelectedDifficulty()
    {
        if (easyToggle.isOn)
        {
            return DifficultyType.Easy;
        }

        if (normalToggle.isOn)
        {
            return DifficultyType.Normal;
        }

        if (hardToggle.isOn)
        {
            return DifficultyType.Hard;
        }

        Debug.LogWarning("難易度が選択されていないため Normal を返します。", this);
        return DifficultyType.Normal;
    }

    public void SetInteractable(bool interactable)
    {
        startButton.interactable = interactable;
        exitButton.interactable = interactable;
        easyToggle.interactable = interactable;
        normalToggle.interactable = interactable;
        hardToggle.interactable = interactable;
    }

    private void OnDestroy()
    {
        difficultyChangedSubject.OnCompleted();
        disposables.Dispose();
        difficultyChangedSubject.Dispose();
    }
}
