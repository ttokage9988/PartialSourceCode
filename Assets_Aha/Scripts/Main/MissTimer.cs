using UnityEngine;
using TMPro;

public class MissTimer : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float timeDuration = 5f;
    public float elapsedTime = 0f;

    public bool IsStarted { get; set; } = false;
    public bool IsFinished { get; set; } = false;

    public void Awake()
    {
        text.enabled = false;
    }

    public void Init()
    {
        IsStarted = false;
        IsFinished = false;
        elapsedTime = 0f;
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
        float t = timeDuration - elapsedTime;
        if (t <= 0f)
        {
            Debug.Log("タイムアウト");
            IsFinished = true;
            IsStarted = false;
            text.enabled = false;
        }
        text.text = t.ToString("0");
    }
}
