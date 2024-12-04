using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [SerializeField] GameObject page1;
    [SerializeField] GameObject page2;

    [SerializeField] TaskCanvasManager taskCanvasManager;
    
    public void Next()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        taskCanvasManager.IsClear = true;
        SoundManager.Instance.LoadClip(2);
    }

    public void Close()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }
}
