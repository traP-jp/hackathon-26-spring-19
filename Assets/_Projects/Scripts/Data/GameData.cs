using R3;
using UnityEngine;

public class GameData : MonoBehaviour
{
    GamePhase phase;
    private DifficultyParam difficulty;
    private float timeLimit;
    private float remainingTime;
    private float elapsedTime;
    private int maxLife;
    private int currentLife;
    private int score;
    private ResultType resultType;
    private ItemCountData itemCountData;
}
