using TMPro;
using UnityEngine;

public class ItemCountViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI alcoholCountText;
    [SerializeField] private TextMeshProUGUI healCountText;
    [SerializeField] private TextMeshProUGUI scoreItemCountText;
    public void SetCounts(int alcoholCount, int healCount, int scoreItemCount)
    {
        alcoholCountText.text = $"{alcoholCount}";
        healCountText.text = $"{healCount}";
        scoreItemCountText.text = $"{scoreItemCount}";
    }
    
    void Start()
    {
        SetCounts(0, 0, 0);
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
}
