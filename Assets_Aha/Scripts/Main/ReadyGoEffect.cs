using UnityEngine;
using UnityEngine.Playables;
using System.Collections;

public class ReadyGoEffect : MonoBehaviour
{

    public PlayableDirector Director;

    private void Start()
    {
        Director.Play();
        StartCoroutine("PlaySE");
        
    }

    IEnumerator PlaySE()
    {
        //無理やりディレイをかける（タイムラインの音がオーディオマネージャーの設定と別になるので）
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlaySE("clock_timer");
    }

}
