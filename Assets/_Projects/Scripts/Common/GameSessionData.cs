public sealed class GameSessionData
{
    public DifficultyType SelectedDifficulty { get; set; } = DifficultyType.Normal;
    public ResultData LastResult { get; set; }
}
