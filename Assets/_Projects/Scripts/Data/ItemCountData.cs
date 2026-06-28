using System;
using System.Collections.Generic;

[Serializable]
public class ItemCountData
{
    public int alcoholCount;
    public int healCount;
    public int scoreItemCount;
    public List<ItemIdCountData> itemIdCounts = new();

    public void InitializeCatalog(IEnumerable<ItemParam> itemParams)
    {
        itemIdCounts ??= new List<ItemIdCountData>();

        foreach (ItemParam itemParam in itemParams)
        {
            if (itemParam == null || string.IsNullOrWhiteSpace(itemParam.id)) continue;
            if (Find(itemParam.id) != null) continue;

            itemIdCounts.Add(new ItemIdCountData
            {
                itemId = itemParam.id,
                displayName = itemParam.displayName,
                itemType = itemParam.itemType,
                count = 0
            });
        }
    }

    public void Increment(ItemParam itemParam)
    {
        if (itemParam == null) throw new ArgumentNullException(nameof(itemParam));

        ItemIdCountData entry = Find(itemParam.id);
        if (entry == null)
        {
            InitializeCatalog(new[] { itemParam });
            entry = Find(itemParam.id);
        }

        if (entry != null) entry.count++;

        switch (itemParam.itemType)
        {
            case ItemType.Alcohol:
                alcoholCount++;
                break;
            case ItemType.Heal:
                healCount++;
                break;
            case ItemType.Score:
                scoreItemCount++;
                break;
        }
    }

    public int GetCount(string itemId)
    {
        return Math.Max(0, Find(itemId)?.count ?? 0);
    }

    public int GetSubtotal(ItemType itemType)
    {
        int subtotal = 0;
        if (itemIdCounts == null) return subtotal;

        foreach (ItemIdCountData entry in itemIdCounts)
        {
            if (entry != null && entry.itemType == itemType)
            {
                subtotal += Math.Max(0, entry.count);
            }
        }
        return subtotal;
    }

    public ItemCountData Clone()
    {
        var clone = new ItemCountData
        {
            alcoholCount = Math.Max(0, alcoholCount),
            healCount = Math.Max(0, healCount),
            scoreItemCount = Math.Max(0, scoreItemCount)
        };

        if (itemIdCounts == null) return clone;

        foreach (ItemIdCountData entry in itemIdCounts)
        {
            if (entry == null) continue;

            clone.itemIdCounts.Add(new ItemIdCountData
            {
                itemId = entry.itemId,
                displayName = entry.displayName,
                itemType = entry.itemType,
                count = Math.Max(0, entry.count)
            });
        }
        return clone;
    }

    private ItemIdCountData Find(string itemId)
    {
        if (itemIdCounts == null || string.IsNullOrWhiteSpace(itemId)) return null;

        foreach (ItemIdCountData entry in itemIdCounts)
        {
            if (entry != null && string.Equals(entry.itemId, itemId, StringComparison.OrdinalIgnoreCase))
            {
                return entry;
            }
        }
        return null;
    }
}

[Serializable]
public sealed class ItemIdCountData
{
    public string itemId;
    public string displayName;
    public ItemType itemType;
    public int count;
}
