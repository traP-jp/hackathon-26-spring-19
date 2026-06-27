using UnityEngine;

public class GameUIViewer : MonoBehaviour
{
    [SerializeField] LifeViewer lifeViewer;
    [SerializeField] TimerViewer timerViewer;
    [SerializeField] ScoreViewer scoreViewer;
    [SerializeField] DifficultyViewer difficultyViewer;
    [SerializeField] ItemCountViewer itemCountViewer;
    [SerializeField] GameData gameData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Refresh(GameData gameData)
    {
        lifeViewer.SetLife(gameData.currentLife, gameData.maxLife);
        timerViewer.SetTime(gameData.remainingTime, gameData.timeLimit);
        scoreViewer.SetScore(gameData.score);
        difficultyViewer.SetDifficulty(gameData.difficulty);
        itemCountViewer.SetItemCounts(gameData.itemCountData);

    }
}
