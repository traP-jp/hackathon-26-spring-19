using TMPro;
using UnityEngine;

public class ItemCountViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI alcoholCountText;
    [SerializeField] private TextMeshProUGUI healCountText;
    [SerializeField] private TextMeshProUGUI scoreItemCountText;

    private void Awake()
    {
        SetCounts(0, 0, 0);
    }

    public void SetCounts(int alcoholCount, int healCount, int scoreItemCount)
    {
        SetText(alcoholCountText, alcoholCount);
        SetText(healCountText, healCount);
        SetText(scoreItemCountText, scoreItemCount);
    }

    public void SetItemCounts(ItemCountData itemCountData)
    {
        if (itemCountData == null)
        {
            SetCounts(0, 0, 0);
            return;
        }

        SetCounts(
            itemCountData.alcoholCount,
            itemCountData.healCount,
            itemCountData.scoreItemCount
        );
    }

    private static void SetText(TextMeshProUGUI target, int count)
    {
        if (target != null) target.text = Mathf.Max(0, count).ToString();
    }
}
