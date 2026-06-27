using VContainer;
using VContainer.Unity;

/// <summary>
/// タイトルシーン内のコンポーネントを登録する Scope。
/// </summary>
public sealed class TitleScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<TitleInfo>();
        builder.RegisterComponentInHierarchy<TitleViewer>();
    }
}
