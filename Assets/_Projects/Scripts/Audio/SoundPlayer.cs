using UnityEngine;

public sealed class SoundPlayer
{
    private readonly SoundParam soundParam;
    private readonly AudioSource seSource;

    //効果音を鳴らす場所を作る
    public SoundPlayer(SoundParam soundParam)
    {
        this.soundParam = soundParam;

        GameObject soundObject = new("SoundPlayer");
        Object.DontDestroyOnLoad(soundObject);

        seSource = soundObject.AddComponent<AudioSource>();
        seSource.playOnAwake = false;
    }

    //ダメージ音を鳴らす
    public void PlayDamage()
    {
        PlaySe(soundParam?.damageSe);
    }

    //回復音を鳴らす
    public void PlayHeal()
    {
        PlaySe(soundParam?.healSe);
    }

    //アイテム取得音を鳴らす
    public void PlayItemGet()
    {
        PlaySe(soundParam?.itemGetSe);
    }

    //クリア音を鳴らす
    public void PlayClear()
    {
        PlaySe(soundParam?.clearSe);
    }

    //ゲームオーバー音を鳴らす
    public void PlayGameOver()
    {
        PlaySe(soundParam?.gameOverSe);
    }

    //ボタン音を鳴らす
    public void PlayButton()
    {
        PlaySe(soundParam?.buttonSe);
    }

    //指定した効果音を再生する
    private void PlaySe(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        seSource.PlayOneShot(clip);
    }
}
