using UnityEngine;
using UnityEngine.UI;
public class TaskCanvasManager : MonoBehaviour
{
    [SerializeField] MainManager mainManager;

    [SerializeField] UICanvasManager uICanvasManager;

    [SerializeField] Text score;

    [SerializeField] GameObject goalCube;

    public GameObject CurrentTaskCube { get; set; }
    public bool IsClear { get; set; } = false;
    public int ClearCount { get; set; } = 0;


    private void OnEnable()
    {
        mainManager.EnableUIMode();
    }

    public void TaskCanvasDisable()
    {
        mainManager.DisableUIMode();

        this.gameObject.SetActive(false);

        //タスクポップアップ非表示
        uICanvasManager.DisableText();

        if (IsClear)
        {
            Destroy(CurrentTaskCube);
            ClearCount++;

            //テキスト更新
            score.text = ClearCount.ToString();

            if (ClearCount >= 8)
            {
                //病院へ行こうと案内
                uICanvasManager.ActiveText(1);

                goalCube.SetActive(true);
            }

            //フラグを戻す
            IsClear = false;
        }
        else
        {
            SoundManager.Instance.LoadClip(1);
        }

    }

}
