using UnityEngine;
using VContainer;
using VContainer.Unity;

/// <summary>
/// シーンをまたいで利用するゲーム設定を登録するルート Scope。
/// </summary>
public sealed class ProjectScope : LifetimeScope
{
    [SerializeField]
    private DifficultyParam[] difficultyParams = System.Array.Empty<DifficultyParam>();

    [SerializeField]
    private ItemParam[] itemParams = System.Array.Empty<ItemParam>();

    [SerializeField]
    private SoundParam soundParam;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<GameSessionData>(Lifetime.Singleton);
        builder.RegisterInstance(difficultyParams);
        builder.RegisterInstance(itemParams);
        builder.RegisterInstance(new SoundPlayer(soundParam));

        if (soundParam != null)
        {
            builder.RegisterInstance(soundParam);
        }
    }
}
