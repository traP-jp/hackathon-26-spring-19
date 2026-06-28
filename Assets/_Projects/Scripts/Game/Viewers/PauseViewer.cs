using UnityEngine;

public sealed class PauseViewer : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void Show()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void Hide()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
    }
}
