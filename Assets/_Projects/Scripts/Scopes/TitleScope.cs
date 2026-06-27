using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

/// <summary>
/// タイトルシーン内のコンポーネントを登録する Scope。
/// </summary>
public sealed class TitleScope : LifetimeScope
{
    [SerializeField]
    private TitleInfo titleInfo;

    [SerializeField]
    private TitleViewer titleViewer;

    [SerializeField]
    private DifficultyParam[] difficultyParams = Array.Empty<DifficultyParam>();

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(titleInfo);
        builder.RegisterComponent(titleViewer);
        builder.RegisterInstance(difficultyParams);
        builder.RegisterEntryPoint<TitlePresentator>();
    }
}
