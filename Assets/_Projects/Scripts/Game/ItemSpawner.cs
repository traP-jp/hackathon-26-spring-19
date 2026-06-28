using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class ItemSpawner : MonoBehaviour, IDisposable
{
    [Header("Prefab")]
    [SerializeField] private FallingItemComponent itemPrefab;
    [SerializeField] private Transform itemParent;

    [Header("Item Params")]
    [SerializeField] private List<ItemParam> alcoholItems = new();
    [SerializeField] private List<ItemParam> healItems = new();
    [SerializeField] private List<ItemParam> scoreItems = new();

    [Header("Spawn Area")]
    [SerializeField] private float spawnY = 5.8f;
    [SerializeField] private float minSpawnX = -8.5f;
    [SerializeField] private float maxSpawnX = 8.5f;
    [SerializeField] private float destroyY = -6.0f;

    [Header("Speed Random")]
    [SerializeField] private float randomSpeedMin = 0.9f;
    [SerializeField] private float randomSpeedMax = 1.1f;

    private readonly List<FallingItemComponent> activeItems = new();
    //アイテムごとの購読を解除するため
    private readonly Dictionary<FallingItemComponent, CompositeDisposable> itemDisposables = new();
    //取得アイテムを外部へ通知するため
    private readonly Subject<ItemParam> itemCollectedSubject = new();

    private DifficultyParam difficulty;
    private float spawnTimer;
    private bool isSpawning;
    private bool hasSpawnedItem;
    //二重破棄を防ぐため
    private bool disposed;

    public Observable<ItemParam> OnItemCollected => itemCollectedSubject;

    public IEnumerable<ItemParam> GetConfiguredItems()
    {
        HashSet<string> yieldedIds = new(StringComparer.OrdinalIgnoreCase);

        foreach (List<ItemParam> category in new[] { alcoholItems, healItems, scoreItems })
        {
            if (category == null) continue;

            foreach (ItemParam item in category)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.id)) continue;
                if (yieldedIds.Add(item.id)) yield return item;
            }
        }
    }

    //難易度で初期化
    public void Initialize(DifficultyParam difficulty)
    {
        if (difficulty == null)
        {
            throw new ArgumentNullException(nameof(difficulty));
        }

        this.difficulty = difficulty;
        spawnTimer = 0f;
        isSpawning = false;
        hasSpawnedItem = false;

        ClearAllItems();
    }

    //生成タイマー更新
    private void Update()
    {
        if (!isSpawning)
        {
            return;
        }

        UpdateSpawnTimer();
    }

    //生成開始
    public void StartSpawn()
    {
        if (difficulty == null)
        {
            Debug.LogError("ItemSpawner is not initialized.", this);
            return;
        }

        if (!HasAnyValidItem())
        {
            Debug.LogError("No valid ItemParam is assigned to ItemSpawner.", this);
            return;
        }

        isSpawning = true;

        if (!hasSpawnedItem)
        {
            SpawnItem();
        }
    }

    //生成停止
    public void StopSpawn()
    {
        isSpawning = false;
    }

    //生成間隔を管理
    public void UpdateSpawnTimer()
    {
        float interval = Mathf.Max(0.01f, difficulty.spawnInterval);

        spawnTimer += Time.deltaTime;

        while (spawnTimer >= interval)
        {
            spawnTimer -= interval;
            SpawnItem();
        }
    }

    //アイテム生成
    public void SpawnItem()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("FallingItem prefab is not assigned.", this);
            StopSpawn();
            return;
        }

        ItemParam itemParam = ChooseItem();

        if (itemParam == null)
        {
            return;
        }

        Vector3 spawnPosition = ChooseSpawnPosition();
        FallingItemComponent item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity, itemParent);
        float fallSpeed = CalculateFallSpeed(itemParam);

        item.Initialize(itemParam, fallSpeed, destroyY);
#if UNITY_EDITOR
        Debug.Log($"Spawned item: {itemParam.id} ({itemParam.itemType})", this);
#endif
        activeItems.Add(item);
        hasSpawnedItem = true;
        RegisterItemEvents(item);
    }

    //割合で選択
    public ItemParam ChooseItem()
    {
        float alcoholRate = CountValidItems(alcoholItems) > 0 ? Mathf.Max(0f, difficulty.alcoholRate) : 0f;
        float healRate = CountValidItems(healItems) > 0 ? Mathf.Max(0f, difficulty.healRate) : 0f;
        float scoreRate = CountValidItems(scoreItems) > 0 ? Mathf.Max(0f, difficulty.scoreRate) : 0f;
        float totalRate = alcoholRate + healRate + scoreRate;

        if (totalRate <= 0f)
        {
            return ChooseRandomItemFromAll();
        }

        float randomValue = UnityEngine.Random.Range(0f, totalRate);

        if (randomValue < alcoholRate)
        {
            return ChooseRandomItemWithFallback(alcoholItems);
        }

        if (randomValue < alcoholRate + healRate)
        {
            return ChooseRandomItemWithFallback(healItems);
        }

        return ChooseRandomItemWithFallback(scoreItems);
    }

    //リストから抽選
    public ItemParam ChooseRandomItem(List<ItemParam> items)
    {
        int validCount = CountValidItems(items);
        if (validCount == 0)
        {
            return null;
        }

        int targetIndex = UnityEngine.Random.Range(0, validCount);
        foreach (ItemParam item in items)
        {
            if (item == null) continue;
            if (targetIndex == 0) return item;
            targetIndex--;
        }

        return null;
    }

    //空なら全体抽選
    private ItemParam ChooseRandomItemWithFallback(List<ItemParam> items)
    {
        ItemParam selectedItem = ChooseRandomItem(items);

        if (selectedItem != null)
        {
            return selectedItem;
        }

        return ChooseRandomItemFromAll();
    }

    //全カテゴリ抽選
    private ItemParam ChooseRandomItemFromAll()
    {
        int alcoholCount = CountValidItems(alcoholItems);
        int healCount = CountValidItems(healItems);
        int scoreCount = CountValidItems(scoreItems);
        int totalCount = alcoholCount + healCount + scoreCount;

        if (totalCount == 0)
        {
            return null;
        }

        int index = UnityEngine.Random.Range(0, totalCount);

        if (index < alcoholCount)
        {
            return GetValidItemAt(alcoholItems, index);
        }

        index -= alcoholCount;

        if (index < healCount)
        {
            return GetValidItemAt(healItems, index);
        }

        index -= healCount;
        return GetValidItemAt(scoreItems, index);
    }

    //生成位置を決定
    public Vector3 ChooseSpawnPosition()
    {
        float x = UnityEngine.Random.Range(minSpawnX, maxSpawnX);
        return new Vector3(x, spawnY, 0f);
    }

    //落下速度を計算
    public float CalculateFallSpeed(ItemParam itemParam)
    {
        float speedMultiplier = itemParam.speedMultiplier <= 0f ? 1f : itemParam.speedMultiplier;
        float min = Mathf.Min(randomSpeedMin, randomSpeedMax);
        float max = Mathf.Max(randomSpeedMin, randomSpeedMax);
        float randomMultiplier = UnityEngine.Random.Range(min, max);

        return difficulty.baseFallSpeed * speedMultiplier * randomMultiplier;
    }

    //通知を購読
    public void RegisterItemEvents(FallingItemComponent item)
    {
        CompositeDisposable disposable = new CompositeDisposable();

        item.OnCollected
            .Subscribe(OnItemCollectedInternal)
            .AddTo(disposable);

        item.OnOutOfScreen
            .Subscribe(OnItemOutOfScreenInternal)
            .AddTo(disposable);

        itemDisposables[item] = disposable;
    }

    //取得時処理
    private void OnItemCollectedInternal(FallingItemComponent item)
    {
        if (!activeItems.Contains(item))
        {
            return;
        }

        itemCollectedSubject.OnNext(item.ItemParam);
        DestroyItem(item);
    }

    //画面外処理
    private void OnItemOutOfScreenInternal(FallingItemComponent item)
    {
        if (!activeItems.Contains(item))
        {
            return;
        }

        DestroyItem(item);
    }

    //アイテム削除
    public void DestroyItem(FallingItemComponent item)
    {
        if (item == null)
        {
            return;
        }

        if (itemDisposables.TryGetValue(item, out CompositeDisposable disposable))
        {
            disposable.Dispose();
            itemDisposables.Remove(item);
        }

        activeItems.Remove(item);
        Destroy(item.gameObject);
    }

    //全アイテム削除
    public void ClearAllItems()
    {
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            DestroyItem(activeItems[i]);
        }

        activeItems.Clear();
        itemDisposables.Clear();
    }

    //購読を破棄
    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        StopSpawn();
        ClearAllItems();
        itemCollectedSubject.Dispose();
    }

    //破棄時に解放
    private void OnDestroy()
    {
        Dispose();
    }

    private bool HasAnyValidItem()
    {
        return CountValidItems(alcoholItems) +
               CountValidItems(healItems) +
               CountValidItems(scoreItems) > 0;
    }

    private static int CountValidItems(List<ItemParam> items)
    {
        if (items == null) return 0;

        int count = 0;
        foreach (ItemParam item in items)
        {
            if (item != null) count++;
        }
        return count;
    }

    private static ItemParam GetValidItemAt(List<ItemParam> items, int targetIndex)
    {
        if (items == null || targetIndex < 0) return null;

        foreach (ItemParam item in items)
        {
            if (item == null) continue;
            if (targetIndex == 0) return item;
            targetIndex--;
        }
        return null;
    }

    private void OnValidate()
    {
        if (minSpawnX > maxSpawnX)
        {
            (minSpawnX, maxSpawnX) = (maxSpawnX, minSpawnX);
        }

        randomSpeedMin = Mathf.Max(0.01f, randomSpeedMin);
        randomSpeedMax = Mathf.Max(0.01f, randomSpeedMax);
    }
}
