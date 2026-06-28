public sealed class GameSessionData
{
    public DifficultyType selectedDifficulty = DifficultyType.Normal;
    public ResultData lastResult { get; private set; }

    public void SaveResult(GameData gameData)
    {
        if (gameData == null)
        {
            throw new System.ArgumentNullException(nameof(gameData));
        }

        lastResult = new ResultData
        {
            resultType = gameData.resultType,
            difficultyType = gameData.difficulty != null
                ? gameData.difficulty.difficultyType
                : selectedDifficulty,
            survivalTime = System.Math.Max(0f, gameData.elapsedTime),
            remainingLife = System.Math.Max(0, gameData.currentLife),
            finalScore = System.Math.Max(0, gameData.score),
            itemCountData = CopyItemCountData(gameData.itemCountData)
        };
    }

    public void ClearResult()
    {
        lastResult = null;
    }

    private static ItemCountData CopyItemCountData(ItemCountData source)
    {
        if (source == null)
        {
            return new ItemCountData();
        }

        return source.Clone();
    }
}
