using Aha.Main.Timer;
using Aha.Main.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Aha.Main
{
    /// <summary>
    /// アハのメインゲームシステム
    /// </summary>
    public class AhaMain : MonoBehaviour
    {
        //システム
        [SerializeField] private AhaAnswer ahaAnswer;
        [SerializeField] private AhaUIManager ahaUIManager;
        [SerializeField] private AhaTimerManager ahaTimerManager;
        [SerializeField] private GameClearManager gameClearManager;

        //演出
        [SerializeField] private ReadyGoEffect readyGoEffect;

        //問題
        [SerializeField] private QuestionCreator questionCreator;

        /// <summary>
        /// お手付きの管理クラス
        /// </summary>
        [SerializeField] private MissManager missManager;

        void Start()
        {
            //例外的に最後はおめでとう表示を行う
            if (gameClearManager.IsGameClear())
            {
                gameClearManager.OpenCompleteResultCanvas();
                return;
            }

            Init();
            Subscribes();
            GameRoutine().Forget();
        }

        private void Init()
        {
            AudioManager.Instance.StopBGM();
            ahaAnswer.enabled = false;
        }

        /// <summary>
        /// ゲーム全体のルーティーン
        /// </summary>
        /// <returns></returns>
        private async UniTask GameRoutine()
        {            
            await readyGoEffect.PlayReadyGo();
            questionCreator.CreateQuestion();
            var isClear = await AhaAnswerPhase();
            if (isClear) Save();
        }

        /// <summary>
        ///　購読処理
        /// </summary>
        private void Subscribes()
        {
            //アハに正解したら正解キャンバスを表示する
            ahaAnswer.IsCorrectedObservable.Where(isCorrected => isCorrected)
                .Subscribe(_ => ahaUIManager.CorrectResultRoutine().Forget())
                .AddTo(this);

            //アハに不正解時、お手付き処理を表示する
            ahaAnswer.IsCorrectedObservable.Where(isCorrected => !isCorrected)
                .Subscribe(_ => missManager.ShowInCorrectEffect())
                .AddTo(this);

            //時間切れの場合、ゲームオーバーを表示する
            ahaTimerManager.MainTimer.TimerEndObservable
                .Subscribe(_ => ahaUIManager.OpenGameOverCanvas())
                .AddTo(this);
            
            //お手付きの間、解答を無効にする
            ahaTimerManager.MissTimer.TimerStartObservable
                .Subscribe(_ => ahaAnswer.enabled = false)
                .AddTo(this);
            ahaTimerManager.MissTimer.TimerEndObservable
                .Subscribe(_ =>
                {
                    ahaAnswer.enabled = true;
                    ahaAnswer.IsAnswered = false; //解答フラグリセット
                })
                .AddTo(this);
        }

        /// <summary>
        /// 解答フェーズ
        /// </summary>
        /// <returns></returns>
        private async UniTask<bool> AhaAnswerPhase()
        {
            ahaAnswer.enabled = true;
            AudioManager.Instance.PlayBGM("main");
            ahaTimerManager.MainTimer.StartTimer();

            //正解（クリア）か、タイムアウト（ゲームオーバー）まで待つ
            var correctedSubject = ahaAnswer.IsCorrectedObservable.Where(isCorrected => isCorrected).Select(_ => true);
            var timeoutSubject = ahaTimerManager.MainTimer.TimerEndObservable.Select(_ => false);
            var merged = Observable.Merge(correctedSubject, timeoutSubject);
            bool isClear = await merged.First().ToUniTask();

            ahaTimerManager.MainTimer.StopTimer();
            ahaAnswer.enabled = false;
            return isClear;
        }

        /// <summary>
        /// セーブ
        /// </summary>
        private void Save()
        {
            Singleton<ScoreManager>.Instance.SetCleared(SceneController.SelectedStageNumber, true);
            Singleton<ScoreManager>.Instance.Save();
        }
    }
}