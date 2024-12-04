using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : MonoBehaviour
{
    [SerializeField] List<GameObject> textList = new List<GameObject>();
    [SerializeField] GameObject goalPanel;
    int numCache;

    public void ActiveText(int num)
    {
        textList[num].SetActive(true);
        numCache = num;
    }

    public void DisableText()
    {
        textList[numCache].SetActive(false);
    }

    public void Goal()
    {
        goalPanel.SetActive(true);
    }
}
