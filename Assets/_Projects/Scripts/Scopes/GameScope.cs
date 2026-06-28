using VContainer;
using VContainer.Unity;

/// <summary>
/// ゲームシーン内の状態と UI コンポーネントを登録する Scope。
/// </summary>
public sealed class GameScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameData>();
        builder.RegisterComponentInHierarchy<PauseInfo>();
        builder.RegisterComponentInHierarchy<GameResultInfo>();
        builder.RegisterComponentInHierarchy<GameResultViewer>();
        builder.RegisterComponentInHierarchy<ItemSpawner>();
        builder.RegisterComponentInHierarchy<PlayerController>();

        builder.RegisterComponentInHierarchy<LifeViewer>();
        builder.RegisterComponentInHierarchy<TimerViewer>();
        builder.RegisterComponentInHierarchy<ScoreViewer>();
        builder.RegisterComponentInHierarchy<DifficultyViewer>();
        builder.RegisterComponentInHierarchy<ItemCountViewer>();
        builder.RegisterComponentInHierarchy<GameUIViewer>();

        builder.Register<TimerSystem>(Lifetime.Singleton);
        builder.Register<ItemEffectSystem>(Lifetime.Singleton);
        builder.Register<GameJudgeSystem>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameMainPresentator>();
    }
}
