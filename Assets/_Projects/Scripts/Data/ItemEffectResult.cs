public class ItemEffectResult
{
    // 取得したアイテムのParam。
    public ItemParam itemParam;

    // 取得したアイテムの分類
    public ItemType itemType;

    // 効果適用前のライフ。
    public int beforeLife;

    // 効果適用後のライフ。
    public int afterLife;
    
    // 実際に変化したライフ量。
    public int lifeDelta;

    // 効果適用前のスコア。
    public int beforeScore;

    // 効果適用後のスコア。
    public int afterScore;

    // 今回加算されたスコア。
    public int addedScore;

    // 酒類などでライフが減ったか。
    public bool isDamage;

    // 回復アイテムを取得したか。
    public bool isHealItem;

    // 実際にライフが回復したか。(体力最大時はfalse)
    public bool isLifeRecovered;

    // スコアアイテムを取得したか。
    public bool isScoreItem;

    // スコアが増えたか。
    public bool isScoreAdded;

    // ライフ最大時に回復アイテムを取得したか。
    public bool isLifeMaxWhenHeal;
}