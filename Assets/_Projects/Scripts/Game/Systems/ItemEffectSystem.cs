using System;

public class ItemEffectSystem
{
    // 取得したアイテムの効果をGameDataに反映し、発生した効果をItemEffectResultとして返す。
    public ItemEffectResult ApplyItemEffect(GameData gameData, ItemParam itemParam)
    {

        EnsureItemCountData(gameData);

        int beforeLife = gameData.currentLife;
        int beforeScore = gameData.score;

        AddItemCount(gameData, itemParam.itemType);

        switch (itemParam.itemType)
        {
            case ItemType.Alcohol:
                ApplyAlcoholEffect(gameData, itemParam);
                break;

            case ItemType.Heal:
                ApplyHealEffect(gameData, itemParam);
                break;

            case ItemType.Score:
                ApplyScoreEffect(gameData, itemParam);
                break;

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(itemParam.itemType),
                    itemParam.itemType,
                    "未対応のItemTypeです。"
                );
        }

        ClampLife(gameData);

        int afterLife = gameData.currentLife;
        int afterScore = gameData.score;

        return new ItemEffectResult
        {
            itemParam = itemParam,
            itemType = itemParam.itemType,

            beforeLife = beforeLife,
            afterLife = afterLife,
            lifeDelta = afterLife - beforeLife,

            beforeScore = beforeScore,
            afterScore = afterScore,
            addedScore = afterScore - beforeScore,

            isDamage = afterLife < beforeLife,
            isHealItem = itemParam.itemType == ItemType.Heal,
            isLifeRecovered = afterLife > beforeLife,
            isScoreItem = itemParam.itemType == ItemType.Score,
            isScoreAdded = afterScore > beforeScore,
            isLifeMaxWhenHeal = itemParam.itemType == ItemType.Heal && beforeLife >= gameData.maxLife
        };
    }

    // 酒類アイテム取得時の処理。ライフ -1、スコア変化なし。
    public void ApplyAlcoholEffect(GameData gameData, ItemParam itemParam)
    {
        // ダメージ量を取得
        int damageValue = itemParam.lifeDelta;

        gameData.currentLife -= damageValue;
    }

    // 回復アイテム取得時の処理。ライフ +1、スコア +100。
    // ライフ最大時はライフを増やさずスコア +150。
    public void ApplyHealEffect(GameData gameData, ItemParam itemParam)
    {
        bool isLifeMax = gameData.currentLife >= gameData.maxLife;

        int addedScore = CalculateAddedScore(gameData, itemParam);
        gameData.score += addedScore;

        if (isLifeMax)
        {
            return;
        }

        // 回復量を取得
        int healValue = itemParam.lifeDelta;

        gameData.currentLife += healValue;
    }

    // スコアアイテム取得時の処理。
    public void ApplyScoreEffect(GameData gameData, ItemParam itemParam)
    {
        int addedScore = CalculateAddedScore(gameData, itemParam);
        gameData.score += addedScore;
    }

    // 取得したアイテムの分類に応じて取得数を加算する。
    public void AddItemCount(GameData gameData, ItemType itemType)
    {
        EnsureItemCountData(gameData);

        switch (itemType)
        {
            case ItemType.Alcohol:
                gameData.itemCountData.alcoholCount++;
                break;

            case ItemType.Heal:
                gameData.itemCountData.healCount++;
                break;

            case ItemType.Score:
                gameData.itemCountData.scoreItemCount++;
                break;

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(itemType),
                    itemType,
                    "未対応のItemType。"
                );
        }
    }

    // アイテム取得時に加算するスコアを計算する。
    public int CalculateAddedScore(GameData gameData, ItemParam itemParam)
    {

        if (itemParam.itemType == ItemType.Alcohol)
        {
            return 0;
        }

        if (itemParam.itemType == ItemType.Heal)
        {
            bool isLifeMax = gameData.currentLife >= gameData.maxLife;

            if (isLifeMax)
            {
                if (itemParam.maxLifeHealScoreValue > 0)
                {
                    return itemParam.maxLifeHealScoreValue;
                }
                else
                {
                    return itemParam.scoreValue;
                }
            }

            return Math.Max(0, itemParam.scoreValue);
        }

        if (itemParam.itemType == ItemType.Score)
        {
            return Math.Max(0, itemParam.scoreValue);
        }

        return 0;
    }

    // 現在ライフを0以上、最大ライフ以下に制限する。
    public void ClampLife(GameData gameData)
    {
        if (gameData.maxLife < 0)
        {
            gameData.maxLife = 0;
        }

        if (gameData.currentLife < 0)
        {
            gameData.currentLife = 0;
        }

        if (gameData.currentLife > gameData.maxLife)
        {
            gameData.currentLife = gameData.maxLife;
        }
    }

    // itemCountDataがnullなら生成する。
    private void EnsureItemCountData(GameData gameData)
    {
        if (gameData.itemCountData == null)
        {
            gameData.itemCountData = new ItemCountData();
        }
    }
}