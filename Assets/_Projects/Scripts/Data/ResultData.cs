using System;

[Serializable]
public sealed class ResultData
{
    public ResultType resultType;
    public DifficultyType difficultyType;
    public float survivalTime;
    public int remainingLife;
    public int finalScore;
    public ItemCountData itemCountData;
}
