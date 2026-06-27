using TMPro;
using UnityEngine;

public class TimerViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    public void SetTime(float remainingTime, float timeLimit)
    {
        if (timerText == null) return;

        float upperBound = timeLimit > 0f ? timeLimit : float.MaxValue;
        float clampedTime = Mathf.Clamp(remainingTime, 0f, upperBound);
        timerText.text = Mathf.CeilToInt(clampedTime).ToString();
    }
}
