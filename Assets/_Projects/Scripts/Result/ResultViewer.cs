using UnityEngine;
using TMPro;

public class ResultViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    [SerializeField] private TextMeshProUGUI survivalTimeText;
    [SerializeField] private TextMeshProUGUI remainingLifeText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    [SerializeField] private ItemCountViewer itemCountViewer;

    public void Show(ResultData data)
    {
        if (data == null)
        {
            Clear();
            return;
        }

        SetResultText(data.resultType);
        SetText(difficultyText, data.difficultyType.ToString());
        SetText(survivalTimeText, FormatTime(data.survivalTime));
        SetText(remainingLifeText, Mathf.Max(0, data.remainingLife).ToString());
        SetText(finalScoreText, data.finalScore.ToString());
        if (itemCountViewer != null)
        {
            itemCountViewer.SetItemCounts(data.itemCountData);
        }
    }

    public void SetResultText(ResultType resultType)
    {
        SetText(resultText, resultType == ResultType.Clear ? "CLEAR" : "GAME OVER");
    }

    public void Clear()
    {
        SetText(resultText, string.Empty);
        SetText(difficultyText, string.Empty);
        SetText(survivalTimeText, string.Empty);
        SetText(remainingLifeText, string.Empty);
        SetText(finalScoreText, string.Empty);
        if (itemCountViewer != null)
        {
            itemCountViewer.SetItemCounts(null);
        }
    }

    private static string FormatTime(float seconds)
    {
        int totalSeconds = Mathf.Max(0, Mathf.FloorToInt(seconds));
        return $"{totalSeconds / 60:00}:{totalSeconds % 60:00}";
    }

    private static void SetText(TextMeshProUGUI target, string value)
    {
        if (target == null)
        {
            return;
        }

        target.text = value;
    }
}
