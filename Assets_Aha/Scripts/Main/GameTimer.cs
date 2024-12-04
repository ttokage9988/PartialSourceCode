using UnityEngine;

public class GameTimer : MonoBehaviour
{
    bool isPaused = false;
    public float elapsedTime = 0f;

    protected void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        isPaused = false;
        Debug.Log("タイマーが開始しました。");
    }

    public void PauseTimer()
    {
        isPaused = true;
        Debug.Log("タイマーが一時停止しました。");
    }

    public void ResumeTimer()
    {
        isPaused = false;
        Debug.Log("タイマーが再開しました。");
    }


    public void ResetTimer()
    {
        elapsedTime = 0;
        Debug.Log("タイマーをリセットしました。");
    }

}