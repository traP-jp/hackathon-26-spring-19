using R3;
using UnityEngine;

public class GameData : MonoBehaviour
{
    GamePhase phase;
    DifficultyParam difficulty;
    float timeLimit;
    float remainingTime;
    float elapsedTime;
    int maxLife;
    int currentLife;
    int score;
    ResultType resultType;
    ItemCountData itemCountData;
}
