using UnityEngine;
using VContainer;
using VContainer.Unity;

/// <summary>
/// リザルトシーン内のデータと UI コンポーネントを登録する Scope。
/// </summary>
public sealed class ResultScope : LifetimeScope
{
    [SerializeField]
    private ResultInfo resultInfo;

    [SerializeField]
    private ResultViewer resultViewer;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(resultInfo);
        builder.RegisterComponent(resultViewer);
        builder.RegisterEntryPoint<ResultPresentator>();
    }
}
