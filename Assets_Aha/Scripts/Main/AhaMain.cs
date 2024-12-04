using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Playables;

public class AhaMain : MonoBehaviour
{
    public AhaAnswer AhaAnswer;
    public AhaTimer AhaTimer;
    public MissTimer MissTimer;

    public QuestionTimeText QuestionTimeText;

    bool isClear = false;

    public Image X;

    //キャンバス
    public GameObject Canvas;
    public GameObject ResultCanvas;
    public GameObject GameOverCanvas;
    public GameObject CompleteResultCanvas;

    //演出
    public ReadyGoEffect ReadyGoEffect;
    public GameObject CorrectPanel;
    public FadeCanvas FadeCanvas;

    // 問題プレハブ情報
    public QuestionStorage QuestionStorage;

    //ミス用
    public GameObject MissCursor;
    public Image MissMark;

    void Start()
    {
        AudioManager.Instance.StopBGM();
        AhaAnswer.enabled = false;
        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        // 最終問かどうかで分岐
        if (SceneController.SelectedStageNumber >= QuestionStorage.Objects.Count)
        {
            OpenCompleteResultCanvas();
        }

        yield return StartPhase();
        AudioManager.Instance.PlayBGM("main");
        yield return AhaWatchPhase();
        yield return AhaAnswerPhase();

        //リザルト（正解orゲームオーバー）
        ResultPhase();
    }

    IEnumerator StartPhase()
    {
        ReadyGoEffect.gameObject.SetActive(true);
        //再生終了まで待つ
        yield return new WaitUntil(() => ReadyGoEffect.Director.time >= ReadyGoEffect.Director.duration);
        ReadyGoEffect.gameObject.SetActive(false);        
    }

    IEnumerator AhaWatchPhase()
    {
        /*
        string questionPrefabName = "Questions/Question" + SceneController.SelectedStageNumber;        
        var question = Resources.Load(questionPrefabName);
        var questionObj = Instantiate(question) as GameObject;
        var AhaScript = questionObj.GetComponent<QuestionPrefab>();
        */
        // 問題プレハブを管理するやつ 経由でプレハブを取得
        var prefab = QuestionStorage.Objects[SceneController.SelectedStageNumber].prefab;
        var AhaScript = Instantiate(prefab);

        // 「出題中」がいらない場合は以下の1行を消す
        //QuestionTimeText.Show();

        //yield return new WaitUntil(() => AhaScript.IsFinished);
        yield return null;
    }

    IEnumerator AhaAnswerPhase()
    {
        AhaAnswer.enabled = true;

        // 「出題中」がいらない場合は以下の1行を消す
        //QuestionTimeText.Hide();
        
        //回答タイマー開始
        AhaTimer.TimerStart();
        
        while (true)
        {
            if (AhaAnswer.IsAnswered)
            {
                if (AhaAnswer.IsCorrect)
                {
                    //正解処理
                    Debug.Log("正解しました。");
                    AhaTimer.PauseTimer();
                    isClear = true;
                    yield return CorrectEffect();
                    yield break;
                }
                else
                {
                    //不正解処理
                    Debug.Log("お手付きです。お手付きタイマー開始");
                    AudioManager.Instance.PlaySE("incorrect");
                    //現回答一時停止
                    AhaAnswer.enabled = false;
                    //AhaTimer.PauseTimer();
                    X.enabled = true;
                    //お手付きタイマー処理
                    MissTimer.Init();
                    MissTimer.TimerStart();
                    MissCursor.SetActive(true);
                    yield return ShowMissMark();
                    yield return new WaitUntil(() => 
                        MissTimer.IsFinished ||
                        AhaTimer.IsFinished);
                    MissCursor.SetActive(false);
                    Debug.Log("お手付き終了、もう一周");
                    AhaAnswer.enabled = true;

                    //再回答処理
                    X.enabled = false;
                    AhaAnswer.IsAnswered = false;
                    //AhaTimer.ResumeTimer();
                }
            }
            if (AhaTimer.IsFinished)
            {
                Debug.Log("じかん切れ、ゲームオーバーです。");
                AhaAnswer.enabled = false;
                isClear = false;
                yield break;
            }
            yield return null;
        }
    }

    void ResultPhase()
    {
        if (isClear)
        {
            // ともかくクリアしたらクリア状況はセーブする
            Singleton<ScoreManager>.Instance.SetCleared(SceneController.SelectedStageNumber, isClear);
            Singleton<ScoreManager>.Instance.Save();

            OpenResultCanvas();
        }
        else
        {
            OpenGameOverCanvas();
        }
    }

    public void OpenResultCanvas()
    {
        Debug.Log("リザルトキャンバス開く");
        FadeCanvas.ShowFadePanel(Canvas, ResultCanvas);
        AudioManager.Instance.PlaySE("correct_voice");
    }

    public void OpenCompleteResultCanvas()
    {
        Debug.Log("完璧リザルトキャンバス開く");
        FadeCanvas.ShowFadePanel(Canvas, CompleteResultCanvas);
        AudioManager.Instance.PlaySE("complete_correct_voice");
    }

    public void OpenGameOverCanvas()
    {
        Debug.Log("ゲームオーバーキャンバス開く");
        GameOverCanvas.SetActive(true);
        AudioManager.Instance.PlaySE("incorrect_voice");
    }

    /// <summary>
    /// 正解演出
    /// </summary>
    IEnumerator CorrectEffect()
    {
        CorrectPanel.SetActive(true);
        AudioManager.Instance.PlaySE("correct");
        yield return new WaitForSeconds(1.2f);
        CorrectPanel.SetActive(false);
    }

    IEnumerator ShowMissMark()
    {
        Vector2 pos = MissMark.rectTransform.position;
        pos.x = AhaAnswer.ClickedPosition.x;
        pos.y = AhaAnswer.ClickedPosition.y;
        MissMark.rectTransform.position = pos;
        MissMark.enabled = true;
        yield return new WaitForSeconds(0.5f);
        MissMark.enabled = false;
    }
}
