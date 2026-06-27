using UnityEngine;
using TMPro;
using R3;
using VContainer;

public class TitleViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI difficultyDescriptionText;

    private readonly CompositeDisposable disposables = new();
    private TitleInfo titleInfo;
    private DifficultyParam[] difficultyParams;

    [Inject]
    public void Construct(TitleInfo titleInfo, DifficultyParam[] difficultyParams)
    {
        this.titleInfo = titleInfo;
        this.difficultyParams = difficultyParams;
    }

    private void Start()
    {
        if (titleInfo == null) return;

        titleInfo.OnDifficultyChanged
            .Subscribe(SetDifficulty)
            .AddTo(disposables);

        SetDifficulty(titleInfo.GetSelectedDifficulty());
    }

    public void SetDifficulty(DifficultyType difficultyType)
    {
        SetDifficulty(FindDifficulty(difficultyType));
    }

    public void SetDifficulty(DifficultyParam difficulty)
    {
        if (difficultyDescriptionText != null)
        {
            difficultyDescriptionText.text = difficulty != null ? difficulty.description : string.Empty;
        }
    }

    private DifficultyParam FindDifficulty(DifficultyType difficultyType)
    {
        if (difficultyParams == null) return null;

        foreach (DifficultyParam difficulty in difficultyParams)
        {
            if (difficulty != null && difficulty.difficultyType == difficultyType) return difficulty;
        }

        return null;
    }

    private void OnDestroy()
    {
        disposables.Dispose();
    }
}
