using UnityEngine;
using TMPro;

public class DifficultyViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI difficultyText;

    public void SetDifficulty(DifficultyParam difficulty)
    {
        difficultyText.text = difficulty.ToString();
    }
}
