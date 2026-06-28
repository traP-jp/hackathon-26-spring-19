using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCountViewer : MonoBehaviour
{
    [Serializable]
    public sealed class ItemCountTextBinding
    {
        public string itemId;
        public TextMeshProUGUI countText;
    }

    [Header("Item ID Counts")]
    [SerializeField] private List<ItemCountTextBinding> itemCountTexts = new();

    [Header("Subtotals")]
    [SerializeField] private TextMeshProUGUI alcoholSubtotalText;
    [SerializeField] private TextMeshProUGUI foodSubtotalText;

    public void SetItemCounts(ItemCountData itemCountData)
    {
        itemCountData ??= new ItemCountData();

        foreach (ItemCountTextBinding binding in itemCountTexts)
        {
            if (binding?.countText == null || string.IsNullOrWhiteSpace(binding.itemId)) continue;
            binding.countText.text = itemCountData.GetCount(binding.itemId).ToString();
        }

        SetText(alcoholSubtotalText, itemCountData.GetSubtotal(ItemType.Alcohol));

        int foodSubtotal = itemCountData.GetSubtotal(ItemType.Heal) +
                           itemCountData.GetSubtotal(ItemType.Score);
        SetText(foodSubtotalText, foodSubtotal);
    }

    private static void SetText(TextMeshProUGUI target, int count)
    {
        if (target != null) target.text = Mathf.Max(0, count).ToString();
    }
}
