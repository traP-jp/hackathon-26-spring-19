using UnityEngine;
using System.Collections.Generic;

public class LifeViewer : MonoBehaviour
{
    [SerializeField] private Transform lifeIconParent;
    [SerializeField] private GameObject lifeIconPrefab;
    private readonly List<GameObject> spawnedIcons = new();

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
    }

    public void ClearIcons()
    {
        foreach (GameObject icon in spawnedIcons)
        {
            if (icon != null) Destroy(icon);
        }
        spawnedIcons.Clear();
    }
}
