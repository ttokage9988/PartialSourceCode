using Aha.Main.Timer;
using TMPro;
using UniRx;
using UnityEngine;

namespace Aha.Main.UI
{
    /// <summary>
    /// お手付きタイマーの表示クラス
    /// </summary>
    public class MissTimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerLabel;
        [SerializeField] private AhaTimerManager ahaTimerManager;

        void Start()
        {
            //生タイマーをかみ砕いて秒単位に変換し、テキストに反映する
            ahaTimerManager.MissTimer.RawTimerObservable.Select(time => Mathf.CeilToInt(time))
            .DistinctUntilChanged()
            .Where(sec => sec >= 0)
            .Subscribe(sec => timerLabel.text = sec.ToString())
            .AddTo(this);
        }
    }
}
