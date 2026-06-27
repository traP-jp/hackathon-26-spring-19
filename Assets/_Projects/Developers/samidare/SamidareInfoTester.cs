using System.Data.Common;
using UnityEngine;
using UnityEngine.Assertions;

public class SamidareInfoTester : MonoBehaviour
{
    [SerializeField] private InfoCatalog infoCatalog;

    private void Start()
    {
        Assert.IsNotNull(infoCatalog, "InfoCatalog is not assigned in the inspector.");
        Assert.IsTrue(infoCatalog.TryGet("item.alcohol", out InfoAsset byID), "Failed to retrieve InfoAsset for ID: item.alcohol");

        Assert.IsTrue(infoCatalog.TryGet<ItemInfo>("item.alcohol", out ItemInfo item));
        Assert.AreEqual(ItemType.Alcohol, item.ItemType);
    
        Assert.IsTrue(infoCatalog.TryGet(ItemType.Alcohol, out ItemInfo byType));
        Assert.AreEqual(item, byType);

        Assert.IsTrue(infoCatalog.TryGet(
            DifficultyType.Easy,
            out DifficultyInfo difficulty));
        Assert.AreEqual(DifficultyType.Easy, difficulty.DifficultyType);

        Assert.IsFalse(infoCatalog.TryGet("missing", out _));
        Assert.IsFalse(
            infoCatalog.TryGet<DifficultyInfo>("item.alcohol", out _)
        );

        Debug.Log("All tests passed successfully.");
    }
}
