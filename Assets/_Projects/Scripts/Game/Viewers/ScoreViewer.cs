using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetScore(int score)
    {
        if (scoreText != null) scoreText.text = score.ToString();
    }
}
