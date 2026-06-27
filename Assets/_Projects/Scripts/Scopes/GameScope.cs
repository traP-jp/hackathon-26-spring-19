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

        builder.RegisterComponentInHierarchy<LifeViewer>();
        builder.RegisterComponentInHierarchy<TimerViewer>();
        builder.RegisterComponentInHierarchy<ScoreViewer>();
        builder.RegisterComponentInHierarchy<DifficultyViewer>();
        builder.RegisterComponentInHierarchy<ItemCountViewer>();
        builder.RegisterComponentInHierarchy<GameUIViewer>();
    }
}
