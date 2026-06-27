using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Game/Info/Item")]
public sealed class ItemInfo : InfoAsset
{
    [SerializeField]
    private ItemType itemType;

    public ItemType ItemType => itemType;
    public string ItemName => itemType.ToString();
}
