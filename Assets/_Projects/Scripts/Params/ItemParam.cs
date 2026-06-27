using UnityEngine;

[CreateAssetMenu(fileName = "ItemParam", menuName = "Scriptable Objects/ItemParam")]
public class ItemParam : ScriptableObject
{
    public string id;
    public string displayName;
    public ItemType itemType;
    public Sprite sprite;
    public int scoreValue;
    public int maxLifeHealScoreValue;
    public int lifeDelta;
    public float speedMultiplier;
}
