using UnityEngine;
using TMPro;

public class AhaTimer : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float timeDuration = 10f;
    public float elapsedTime = 0f;

    public bool IsStarted { get; set; } = false;
    public bool IsFinished { get; set; } = false;

    public void Awake()
    {
        text.enabled = false;
    }

    public void TimerStart()
    {
        text.enabled = true;
        IsStarted = true;
    }

    public void PauseTimer()
    {
        IsStarted = false;
    }

    void Update()
    {
        if (!IsStarted || IsFinished) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Round(timeDuration - elapsedTime + 0.5f);
        if(t <= 0f)
        {
            Debug.Log("タイムアウト");
            IsFinished = true;
            IsStarted = false;
        }
        text.text = t.ToString("00");
    }
}
