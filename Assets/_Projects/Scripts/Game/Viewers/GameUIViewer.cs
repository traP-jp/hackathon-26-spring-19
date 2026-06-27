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

    private void Start()
    {
        Refresh();
    }

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (gameData != null) Refresh(gameData);
    }

    public void Refresh(GameData data)
    {
        if (data == null) return;

        lifeViewer?.SetLife(data.currentLife, data.maxLife);
        timerViewer?.SetTime(data.remainingTime, data.timeLimit);
        scoreViewer?.SetScore(data.score);
        difficultyViewer?.SetDifficulty(data.difficulty);
        itemCountViewer?.SetItemCounts(data.itemCountData);
    }
}
