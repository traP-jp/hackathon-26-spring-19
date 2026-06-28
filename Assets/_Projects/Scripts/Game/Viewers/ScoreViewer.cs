using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string prefix = "";
    [SerializeField] private Vector2 fallbackPosition = new(0f, -55f);

    private void Awake()
    {
        if (scoreText == null)
        {
            scoreText = CreateFallbackScoreText();
        }

        SetScore(0);
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"{prefix}{Mathf.Max(0, score):N0}";
        }
    }

    private TextMeshProUGUI CreateFallbackScoreText()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogWarning("ScoreViewer could not find a Canvas.", this);
            return null;
        }

        var scoreObject = new GameObject("ScoreText", typeof(RectTransform));
        scoreObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = scoreObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 1f);
        rect.anchorMax = new Vector2(0.5f, 1f);
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchoredPosition = fallbackPosition;
        rect.sizeDelta = new Vector2(420f, 70f);

        TextMeshProUGUI text = scoreObject.AddComponent<TextMeshProUGUI>();
        text.fontSize = 42f;
        text.alignment = TextAlignmentOptions.Center;
        text.color = Color.black;
        text.raycastTarget = false;
        return text;
    }
}
