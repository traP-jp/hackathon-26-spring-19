using UnityEngine;

[CreateAssetMenu(fileName = "SoundParam", menuName = "Scriptable Objects/SoundParam")]
public class SoundParam : ScriptableObject
{
    public AudioClip titleBgm;
    public AudioClip gameBgm;
    public AudioClip resultBgm;
    [Tooltip("ダメージ時: ビープ音4")]
    public AudioClip damageSe;
    [Tooltip("回復時: ゲージ回復1")]
    public AudioClip healSe;
    [Tooltip("アイテム取得: 決定ボタンを押す42")]
    public AudioClip itemGetSe;
    [Tooltip("クリア時: Phrase05-1")]
    public AudioClip clearSe;
    [Tooltip("ゲームオーバー時: Phrase04-1")]
    public AudioClip gameOverSe;
    [Tooltip("ボタン選択: 決定ボタンを押す31")]
    public AudioClip buttonSe;
}
