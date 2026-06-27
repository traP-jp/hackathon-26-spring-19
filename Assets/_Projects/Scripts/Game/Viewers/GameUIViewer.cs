using UnityEngine;
using VContainer;

public class GameUIViewer : MonoBehaviour
{
    private LifeViewer lifeViewer;
    private TimerViewer timerViewer;
    private ScoreViewer scoreViewer;
    private DifficultyViewer difficultyViewer;
    private ItemCountViewer itemCountViewer;
    private GameData gameData;

    [Inject]
    public void Construct(
        LifeViewer lifeViewer,
        TimerViewer timerViewer,
        ScoreViewer scoreViewer,
        DifficultyViewer difficultyViewer,
        ItemCountViewer itemCountViewer,
        GameData gameData)
    {
        this.lifeViewer = lifeViewer;
        this.timerViewer = timerViewer;
        this.scoreViewer = scoreViewer;
        this.difficultyViewer = difficultyViewer;
        this.itemCountViewer = itemCountViewer;
        this.gameData = gameData;
    }

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
