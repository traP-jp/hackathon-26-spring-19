using System;

public class GameJudgeSystem
{
    public bool IsGameOver(GameDate gameDate)
    {
        if (gameDate == null)
        {
            // エラー時の処理
            throw new ArgumentNullException(nameof(gameData));
        }

        return gameDate.currentLife <= 0;
    }

}