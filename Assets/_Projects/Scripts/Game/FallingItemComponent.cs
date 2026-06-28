using System;
using R3;
using UnityEngine;

public class FallingItemComponent : MonoBehaviour, IDisposable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D hitbox;

    private readonly Subject<FallingItemComponent> collectedSubject = new();
    private readonly Subject<FallingItemComponent> outOfScreenSubject = new();

    private ItemParam itemParam;
    private float fallSpeed;
    private float destroyY;
    private bool isActive;
    private bool disposed;

    public Observable<FallingItemComponent> OnCollected => collectedSubject;
    public Observable<FallingItemComponent> OnOutOfScreen => outOfScreenSubject;
    public ItemParam ItemParam => itemParam;

    //表示部品を取得
    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (hitbox == null)
        {
            hitbox = GetComponent<Collider2D>();
        }
    }

    //落下情報を初期化
    public void Initialize(ItemParam itemParam, float fallSpeed, float destroyY)
    {
        if (itemParam == null)
        {
            throw new ArgumentNullException(nameof(itemParam));
        }

        this.itemParam = itemParam;
        this.fallSpeed = fallSpeed;
        this.destroyY = destroyY;
        isActive = true;

        SetView(itemParam);
    }

    //落下処理を更新
    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        Fall();
        CheckOutOfScreen();
    }

    //下方向に移動
    private void Fall()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
    }

    //画面外を判定
    private void CheckOutOfScreen()
    {
        if (transform.position.y > destroyY)
        {
            return;
        }

        isActive = false;
        outOfScreenSubject.OnNext(this);
    }

    //取得を通知
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
        {
            return;
        }

        if (other.GetComponentInParent<PlayerController>() == null)
        {
            return;
        }

        isActive = false;
        collectedSubject.OnNext(this);
    }

    //見た目を反映
    private void SetView(ItemParam itemParam)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = itemParam.sprite;
            FitHitboxToSprite(itemParam.sprite);
        }
    }

    private void FitHitboxToSprite(Sprite sprite)
    {
        if (hitbox == null || sprite == null) return;

        Bounds bounds = sprite.bounds;

        if (hitbox is BoxCollider2D box)
        {
            box.offset = bounds.center;
            box.size = bounds.size;
        }
        else if (hitbox is CircleCollider2D circle)
        {
            circle.offset = bounds.center;
            circle.radius = Mathf.Min(bounds.extents.x, bounds.extents.y) * 0.85f;
        }
    }

    //通知を破棄
    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        isActive = false;

        collectedSubject.Dispose();
        outOfScreenSubject.Dispose();
    }

    //破棄時に解放
    private void OnDestroy()
    {
        Dispose();
    }
}
