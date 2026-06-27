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
        builder.RegisterComponentInHierarchy<GameUIViewer>();
        builder.RegisterComponentInHierarchy<PauseInfo>();
    }
}
