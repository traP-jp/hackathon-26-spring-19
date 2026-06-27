using UnityEngine;

[CreateAssetMenu(fileName = "SoundParam", menuName = "Scriptable Objects/SoundParam")]
public class SoundParam : ScriptableObject
{
    public AudioClip titleBgm;
    public AudioClip gameBgm;
    public AudioClip resultBgm;
    public AudioClip damageSe;
    public AudioClip healSe;
    public AudioClip itemGetSe;
    public AudioClip clearSe;
    public AudioClip gameOverSe;
    public AudioClip buttonSe;
}
