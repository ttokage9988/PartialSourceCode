using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    [SerializeField] GameObject HowToCanvas;

    private void Start()
    {
        //SoundManager.Instance.LoadBGM(0); //タイトルBGM
    }

    public void ShowHowToCanvas()
    {
        SoundManager.Instance.LoadClip(0);
        HowToCanvas.SetActive(true);        
    }

    public void NextScene()
    {
        SceneController.Instance.NextScene();
    }
}
