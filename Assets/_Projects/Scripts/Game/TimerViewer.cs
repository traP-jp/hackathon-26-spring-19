using UnityEngine;
using TMPro;

public class TimerViewer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    Image timerGauge;
    void SetTime(float remainingTime, float timeLimit){
        if(timerText != null){
            // 0以下で0を表示,そうでなければ残り時間を表示
            timerText.text = (Mathf.Max(0f,remainingTime)).ToString("F1");
        }

        timerGauge.fillAmount = Mathf.Clamp01(remainingTime / timeLimit);

    }

}
