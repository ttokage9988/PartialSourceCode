using UnityEngine;

namespace Aha.Main.Timer
{
    /// <summary>
    /// アハのタイマーを管理するクラス
    /// </summary>
    class AhaTimerManager : MonoBehaviour
    {
        public AhaTimer MainTimer { get; private set; }
        public AhaTimer MissTimer { get; private set; }

        [SerializeField] private float mainTimeDuration = 30f;
        [SerializeField] private float missTimeDuration = 5f;

        private void Awake()
        {
            MainTimer = new AhaTimer(mainTimeDuration);
            MissTimer = new AhaTimer(missTimeDuration);
        }
    }
}
