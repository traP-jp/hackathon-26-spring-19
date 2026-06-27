using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InfoAsset を ID または型から取得するための一覧。
/// 未登録・重複 ID は例外ではなく検索失敗として扱う。
/// </summary>
[CreateAssetMenu(fileName = "InfoCatalog", menuName = "Game/Info/Catalog")]
public sealed class InfoCatalog : ScriptableObject
{
    [SerializeField]
    private List<InfoAsset> entries = new();

    private readonly Dictionary<string, InfoAsset> entriesById = new(StringComparer.Ordinal);
    private bool isCacheBuilt;

    public IReadOnlyList<InfoAsset> Entries => entries;

    public bool TryGet(string id, out InfoAsset info)
    {
        EnsureCache();

        if (string.IsNullOrWhiteSpace(id))
        {
            info = null;
            return false;
        }

        return entriesById.TryGetValue(id, out info) && info != null;
    }

    public bool TryGet<TInfo>(string id, out TInfo info) where TInfo : InfoAsset
    {
        if (TryGet(id, out InfoAsset found) && found is TInfo typedInfo)
        {
            info = typedInfo;
            return true;
        }

        info = null;
        return false;
    }

    public bool TryGet<TInfo>(Predicate<TInfo> match, out TInfo info) where TInfo : InfoAsset
    {
        if (match == null)
        {
            throw new ArgumentNullException(nameof(match));
        }

        foreach (InfoAsset entry in entries)
        {
            if (entry is TInfo typedInfo && match(typedInfo))
            {
                info = typedInfo;
                return true;
            }
        }

        info = null;
        return false;
    }

    public bool TryGet(ItemType itemType, out ItemInfo info)
    {
        return TryGet(item => item.ItemType == itemType, out info);
    }

    public bool TryGet(DifficultyType difficultyType, out DifficultyInfo info)
    {
        return TryGet(difficulty => difficulty.DifficultyType == difficultyType, out info);
    }

    private void OnEnable()
    {
        InvalidateCache();
    }

    private void OnValidate()
    {
        InvalidateCache();
    }

    private void EnsureCache()
    {
        if (isCacheBuilt)
        {
            return;
        }

        entriesById.Clear();

        foreach (InfoAsset entry in entries)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.Id))
            {
                continue;
            }

            if (entriesById.ContainsKey(entry.Id))
            {
                // 重複時に偶然どちらかを返すと設定ミスに気付きにくいため、検索失敗にする。
                entriesById[entry.Id] = null;
                continue;
            }

            entriesById.Add(entry.Id, entry);
        }

        isCacheBuilt = true;
    }

    private void InvalidateCache()
    {
        isCacheBuilt = false;
        entriesById.Clear();
    }
}
