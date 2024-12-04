using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class EndManager : MonoBehaviour
{
    [SerializeField] float duration = 2.0f;

    [SerializeField] List<Image> endingImageList = new List<Image>();

    [SerializeField] GameObject endingCanvas;
    [SerializeField] GameObject endrollCanvas;


    public Image image;
    public async UniTask FadeOut(Image image)
    {
        await image.DOFade(0.0f, duration);
    }

    public async UniTask FadeIn(Image image)
    {
        await image.DOFade(1.0f, duration);
    }

    public async void Start()
    {
        //エンディング再生
        await Ending();

        endingCanvas.SetActive(false);
        endrollCanvas.SetActive(true);

        //エンドロール再生
        await Endroll();

        SceneController.Instance.NextScene();
    }

    private async UniTask Ending()
    {
        SoundManager.Instance.LoadBGM(3);        
        await ShowFadeImage(endingImageList[0]);
        await ShowFadeImage(endingImageList[1]);
        SoundManager.Instance.LoadBGM(4);
        SoundManager.Instance.LoadClip(3);
        await ShowFadeImage(endingImageList[2]);
        await ShowFadeImage(endingImageList[3]);
        SoundManager.Instance.LoadClip(4);
        await ShowFadeImage(endingImageList[4]);
        SoundManager.Instance.LoadClip(4);
        await ShowFadeImage(endingImageList[5]);
        SoundManager.Instance.LoadClip(5);
        await ShowFadeImage(endingImageList[6]);
        SoundManager.Instance.LoadClip(6);
        MoorkaVoice().Forget();
        await ShowFadeImage(endingImageList[7]);
    }

    private async UniTask Endroll()
    {
        //エンドロール音楽再生
        SoundManager.Instance.LoadBGM(1);

        await UniTask.Delay(5000);
    }

    private async UniTask ShowFadeImage(Image image)
    {
        await FadeIn(image);
        await FadeOut(image);
    }

    private async UniTask MoorkaVoice()
    {
        await UniTask.Delay(1000);
        SoundManager.Instance.LoadClip(7);
        await UniTask.Delay(300);
        SoundManager.Instance.LoadClip(7);
        await UniTask.Delay(300);
        SoundManager.Instance.LoadClip(7);
        await UniTask.Delay(300);
        SoundManager.Instance.LoadClip(7);
    }
}
