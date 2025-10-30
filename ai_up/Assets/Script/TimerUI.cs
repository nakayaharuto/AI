using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
            return;

        float t = GameManager.Instance.ElapsedTime;
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int millis = Mathf.FloorToInt((t * 1000) % 1000);

        timerText.text = $"{minutes:00}:{seconds:00}.{millis / 10:00}";
    }
}
