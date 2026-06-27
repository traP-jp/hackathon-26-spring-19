using TMPro;
using UnityEngine;

public class TimerViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    public void SetTime(float remainingTime, float timeLimit)
    {
        timerText.text = remainingTime.ToString("F0");
    }
}
