using TMPro;
using UnityEngine;

public class TitleViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI difficultyDescriptionText;
    [SerializeField] private TextMeshProUGUI difficultyDescriptionText_shade;

    public void SetDifficulty(DifficultyParam difficulty)
    {
        if (difficultyDescriptionText == null)
        {
            Debug.LogWarning("difficultyDescriptionText が設定されていません", this);
            return;
        }

        difficultyDescriptionText.text = difficulty != null
            ? difficulty.description
            : string.Empty;
        difficultyDescriptionText_shade.text = difficulty != null
            ? difficulty.description
            : string.Empty;
    }
}
