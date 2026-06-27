using System;

public class GameJudgeSystem
{
    public bool IsGameOver(GameDate gameDate)
    {
        if (gameDate == null)
        {
            // エラー処理
            throw new ArgumentNullException(nameof(gameData));
        }

        return gameDate.currentLife <= 0;
    }

    public bool IsClear(GameData gameData)
    {
        if (gameData == null)
        {
            // エラー処理
            throw new ArgumentNullException(nameof(gameData));
        }

        return gameData.remainingTime <= 0f && gameData.currentLife > 0;
    }
}