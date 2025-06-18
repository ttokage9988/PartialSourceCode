using Aha.Main.Timer;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Aha.Main
{
    /// <summary>
    /// お手付きの管理クラス    
    /// </summary>
    public class MissManager : MonoBehaviour
    {
        [SerializeField] private Image ahaTimerStopMark;

        [SerializeField] private GameObject missCursor;
        [SerializeField] private Image missMark;

        /// <summary>
        /// アハの正解 / 不正解を判定するクラス
        /// </summary>
        [SerializeField] private AhaAnswer ahaAnswer;

        [SerializeField] private AhaTimerManager ahaTimerManager;

        private CancellationTokenSource cts;

        private void Awake()
        {
            this.enabled = false;
        }

        private void Start()
        {
            ahaTimerManager.MainTimer.TimerEndObservable
                .Subscribe(_ => cts?.Cancel())
                .AddTo(this);
        }

        private void OnEnable()
        {
            MissRoutine();
        }

        private void OnDisable()
        {
            cts?.Cancel();
        }

        private async void MissRoutine()
        {            
            cts = new CancellationTokenSource();

            try
            {
                ahaTimerManager.MissTimer.StartTimer();

                ShowMissEffect();

                await WaitForMisstimerOrMainTimerToFinish(cts.Token);                
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Miss処理中断");
            }
            EndRoutine();
            this.enabled = false;
        }

        /// <summary>
        /// お手付き中に表示する演出群
        /// </summary>
        private void ShowMissEffect()
        {
            ahaTimerStopMark.enabled = true;
            missCursor.SetActive(true);
            ShowXMissPosition().Forget();
        }

        /// <summary>
        /// X印をミスした場所に表示する
        /// </summary>
        /// <returns></returns>
        private async UniTask ShowXMissPosition()
        {
            Vector2 pos = missMark.rectTransform.position;
            pos.x = ahaAnswer.ClickedPosition.x;
            pos.y = ahaAnswer.ClickedPosition.y;
            missMark.rectTransform.position = pos;
            missMark.enabled = true;
            await UniTask.Delay(500);
            missMark.enabled = false;
        }

        /// <summary>
        /// ミスタイマーか、メインタイマーの終了を待つ
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async UniTask WaitForMisstimerOrMainTimerToFinish(CancellationToken token)
        {
            var missTimerFinishedSubject = ahaTimerManager.MissTimer.TimerEndObservable;
            var ahaTimerFinishedSubject = ahaTimerManager.MainTimer.TimerEndObservable;
            var merged = Observable.Merge(missTimerFinishedSubject, ahaTimerFinishedSubject);
            await merged.First().ToUniTask(cancellationToken: token);
        }

        /// <summary>
        /// 後処理のルーティーン
        /// </summary>
        private void EndRoutine()
        {
            missCursor.SetActive(false);
            ahaTimerStopMark.enabled = false;
            ahaTimerManager.MissTimer.StopTimer();
        }

        /// <summary>
        /// 不正解演出
        /// </summary>
        public void ShowInCorrectEffect()
        {
            AudioManager.Instance.PlaySE("incorrect");
            this.enabled = true;
        }
    }
}