using System;
using System.Xml.XPath;

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

    public bool IsFinished(GameData gameData)
    {
        if (gameData == null)
        {
            // エラー処理
            throw new ArgumentNullException(nameof(gameData));
        }

        return IsGameOver(gameData) || IsClear(gameData);
    }

    public ResultType JudgeResult(GameData gameData)
    {
        if (gameData == null)
        {
            // エラー処理
            throw new ArgumentNullException(nameof(gameData));
        }

        if (IsGameOver(gameOver))
        {
            return ResultType.GameOver;
        }

        if (IsClear(gameData))
        {
            return ResultType.Clear;
        }
    }
}