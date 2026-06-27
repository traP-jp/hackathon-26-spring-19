using UnityEngine;
using TMPro;

public class DifficultyViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI difficultyText;

    public void SetDifficulty(DifficultyParam difficulty)
    {
        if (difficultyText != null)
        {
            difficultyText.text = difficulty != null ? difficulty.displayName : string.Empty;
        }
    }
}
