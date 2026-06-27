using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyParam", menuName = "Scriptable Objects/DifficultyParam")]
public class DifficultyParam : ScriptableObject
{
    public DifficultyType difficultyType;
    public string displayName;
    public string description;
    public float timeLimit;
    public float spawnInterval;
    public float baseFallSpeed;
    public float alcoholRate;
    public float healRate;
    public float scoreRate;
}
