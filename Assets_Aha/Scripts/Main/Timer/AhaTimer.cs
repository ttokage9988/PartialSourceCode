using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Aha.Main.Timer
{
    /// <summary>
    /// アハ用のタイマー
    /// </summary>
    public class AhaTimer
    {
        private float timeDuration;
        private CancellationTokenSource cts;
        private bool isPaused = false;
        private float remainingTime;

        
        private Subject<Unit> timerPauseSubject = new Subject<Unit>();
        private Subject<Unit> timerResumeSubject = new Subject<Unit>();

        private Subject<Unit> timerStopSubject = new Subject<Unit>();
        private Subject<Unit> timerStartSubject = new Subject<Unit>();
        private Subject<Unit> timerEndSubject = new Subject<Unit>();
        private Subject<float> rawTimerSubject = new Subject<float>();
        public IObservable<Unit> TimerStartObservable => timerStartSubject;
        public IObservable<Unit> TimerEndObservable => timerEndSubject;
        public IObservable<float> RawTimerObservable => rawTimerSubject;

        public AhaTimer(float timeDuration)
        {
            this.timeDuration = timeDuration;
        }

        private async UniTask StartTimerAsync()
        {
            remainingTime = timeDuration;

            cts?.Cancel();
            cts = new CancellationTokenSource();

            try
            {
                timerStartSubject.OnNext(Unit.Default);
                while (remainingTime > 0)
                {
                    // 一定時間ごとに更新（1フレーム or 固定時間）
                    await UniTask.Yield(PlayerLoopTiming.Update, cts.Token);

                    cts.Token.ThrowIfCancellationRequested();

                    if (isPaused) continue;

                    remainingTime -= Time.deltaTime;
                    //Debug.Log($"残り時間: {remainingTime:F2}");
                    rawTimerSubject.OnNext(remainingTime);
                }

                Debug.Log("タイマー終了");
                timerEndSubject.OnNext(Unit.Default);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("タイマー中断");
            }
        }

        public void StartTimer()
        {
            isPaused = false;
            StartTimerAsync().Forget();
        }

        public void PauseTimer()
        {
            isPaused = true;
            timerPauseSubject.OnNext(Unit.Default);
        }

        public void ResumeTimer()
        {
            isPaused = false;
            timerResumeSubject.OnNext(Unit.Default);
        }

        public void StopTimer()
        {
            cts?.Cancel();
            timerStopSubject.OnNext(Unit.Default);
        }
    }
}
