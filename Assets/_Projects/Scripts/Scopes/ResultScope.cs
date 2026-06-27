using VContainer;
using VContainer.Unity;

/// <summary>
/// リザルトシーン内のデータと UI コンポーネントを登録する Scope。
/// </summary>
public sealed class ResultScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<ResultData>();
        builder.RegisterComponentInHierarchy<ResultInfo>();
        builder.RegisterComponentInHierarchy<ResultViewer>();
    }
}
