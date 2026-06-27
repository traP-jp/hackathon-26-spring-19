using R3;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public GamePhase phase;
    public DifficultyParam difficulty;
    public float timeLimit;
    public float remainingTime;
    public float elapsedTime;
    public int maxLife;
    public int currentLife;
    public int score;
    public ResultType resultType;
    public ItemCountData itemCountData;
}
