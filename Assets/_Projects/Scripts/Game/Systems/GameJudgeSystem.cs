using System;
using System.Xml.XPath;

public class GameJudgeSystem
{
    public bool IsGameOver(GameData gameData)
    {
        if (gameData == null)
        {
            // エラー処理
            throw new ArgumentNullException(nameof(gameData));
        }

        return gameData.currentLife <= 0;
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

        if (IsGameOver(gameData))
        {
            return ResultType.GameOver;
        }

        if (IsClear(gameData))
        {
            return ResultType.Clear;
        }

        throw new InvalidOperationException(
            "エラー：終了条件を満たしていない状態でJudgeResultが呼ばれた"
        );
    }

    public void ApplyResult(GameData gameData, ResultType resultType)
    {
        if (gameData == null)
        {
            // エラー処理
            throw new ArgumentNullException(nameof(gameData));
        }

        gameData.resultType = resultType;
        gameData.phase = GamePhase.Finished;

        // 負の値を取らないようにする
        if (gameData.remainingTime < 0f)
        {
            gameData.remainingTime = 0f;
        }

        if (gameData.currentLife < 0)
        {
            gameData.currentLife = 0;
        }
    }
}