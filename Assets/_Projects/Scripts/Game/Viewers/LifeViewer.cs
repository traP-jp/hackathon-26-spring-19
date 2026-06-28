using UnityEngine;
using System.Collections.Generic;

public class LifeViewer : MonoBehaviour
{
    [SerializeField] private RectTransform lifeIconParent;
    [SerializeField] private GameObject lifeIconPrefab;
    [SerializeField] private float leftPadding;
    [SerializeField, Min(0f)] private float spacing = 12f;
    [SerializeField] private float verticalOffset;
    private readonly List<GameObject> spawnedIcons = new();

    private void Awake()
    {
        if (lifeIconParent == null) return;

        for (int i = 0; i < lifeIconParent.childCount; i++)
        {
            spawnedIcons.Add(lifeIconParent.GetChild(i).gameObject);
        }

        LayoutIcons();
    }

    public void SetLife(int currentLife, int maxLife) 
    {
        int iconCount = Mathf.Clamp(currentLife, 0, Mathf.Max(0, maxLife));
        if (lifeIconParent == null || lifeIconPrefab == null) return;

        while (spawnedIcons.Count < iconCount)
        {
            spawnedIcons.Add(Instantiate(lifeIconPrefab, lifeIconParent));
        }

        while (spawnedIcons.Count > iconCount)
        {
            int lastIndex = spawnedIcons.Count - 1;
            GameObject icon = spawnedIcons[lastIndex];
            spawnedIcons.RemoveAt(lastIndex);
            Destroy(icon);
        }

        LayoutIcons();
    }

    public void ClearIcons()
    {
        foreach (GameObject icon in spawnedIcons)
        {
            if (icon != null) Destroy(icon);
        }
        spawnedIcons.Clear();
    }

    private void LayoutIcons()
    {
        float cursorX = leftPadding;

        foreach (GameObject icon in spawnedIcons)
        {
            if (icon == null || !icon.TryGetComponent(out RectTransform rect)) continue;

            rect.anchorMin = new Vector2(0f, 0.5f);
            rect.anchorMax = new Vector2(0f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localScale = Vector3.one;
            rect.anchoredPosition = new Vector2(cursorX + rect.rect.width * 0.5f, verticalOffset);
            cursorX += rect.rect.width + spacing;
        }
    }
}
