using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyInfo", menuName = "Game/Info/Difficulty")]
public sealed class DifficultyInfo : InfoAsset
{
    [SerializeField]
    private DifficultyType difficultyType;

    public DifficultyType DifficultyType => difficultyType;
}
