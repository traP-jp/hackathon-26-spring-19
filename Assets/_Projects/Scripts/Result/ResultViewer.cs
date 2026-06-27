using UnityEngine;
using TMPro;
using VContainer;

public class ResultViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI survivalTimeText;
    [SerializeField] private TextMeshProUGUI remainingLifeText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private ItemCountViewer itemCountViewer;
    private ResultData resultData;
    private DifficultyParam[] difficultyParams;

    [Inject]
    public void Construct(ItemCountViewer itemCountViewer, ResultData resultData, DifficultyParam[] difficultyParams)
    {
        this.itemCountViewer = itemCountViewer;
        this.resultData = resultData;
        this.difficultyParams = difficultyParams;
    }

    private void Start()
    {
        Show(resultData);
    }

    public void Show(ResultData data)
    {
        if (data == null)
        {
            Clear();
            return;
        }

        SetResultText(data.resultType);
        SetText(difficultyText, GetDifficultyName(data.difficultyType));
        SetText(survivalTimeText, FormatTime(data.survivalTime));
        SetText(remainingLifeText, Mathf.Max(0, data.remainingLife).ToString());
        SetText(finalScoreText, data.finalScore.ToString());
        itemCountViewer?.SetItemCounts(data.itemCountData);
    }

    public void SetResultText(ResultType resultType)
    {
        SetText(resultText, resultType == ResultType.Clear ? "CLEAR" : "GAME OVER");
    }

    private string GetDifficultyName(DifficultyType difficultyType)
    {
        if (difficultyParams != null)
        {
            foreach (DifficultyParam difficulty in difficultyParams)
            {
                if (difficulty != null && difficulty.difficultyType == difficultyType) return difficulty.displayName;
            }
        }

        return difficultyType.ToString();
    }

    private static string FormatTime(float seconds)
    {
        int totalSeconds = Mathf.Max(0, Mathf.FloorToInt(seconds));
        return $"{totalSeconds / 60:00}:{totalSeconds % 60:00}";
    }

    private void Clear()
    {
        SetText(resultText, string.Empty);
        SetText(difficultyText, string.Empty);
        SetText(survivalTimeText, string.Empty);
        SetText(remainingLifeText, string.Empty);
        SetText(finalScoreText, string.Empty);
        itemCountViewer?.SetItemCounts(null);
    }

    private static void SetText(TextMeshProUGUI target, string value)
    {
        if (target != null) target.text = value;
    }
}
