using UnityEngine;

public class GoalCube : MonoBehaviour
{
    bool flag = false;
    private void OnTriggerStay(Collider other)
    {
        //ゴール処理
        FindObjectOfType<UICanvasManager>().Goal();
        FindObjectOfType<MainManager>().EnableUIMode();

        if (!flag)
        {
            //ドンパフ
            SoundManager.Instance.LoadClip(8);
            flag = true;
        }
    }
}
