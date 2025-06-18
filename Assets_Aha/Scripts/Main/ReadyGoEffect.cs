using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

namespace Aha.Main
{
    /// <summary>
    /// レディーゴー演出を行うクラス
    /// </summary>
    public class ReadyGoEffect : MonoBehaviour
    {
        [SerializeField] private PlayableDirector director;

        private void Start()
        {
            director.Play();
            PlaySE().Forget();
        }

        /// <summary>
        /// レディーゴー演出を再生する
        /// </summary>
        /// <returns></returns>
        public async UniTask PlayReadyGo()
        {
            this.gameObject.SetActive(true);
            director.Play();
            PlaySE().Forget();

            await UniTask.WaitUntil(() => director.time >= director.duration);
            this.gameObject.SetActive(false);
        }

        private async UniTask PlaySE()
        {
            //無理やりディレイをかける（タイムラインの音がオーディオマネージャーの設定と別になるため）
            await UniTask.Delay(500);
            AudioManager.Instance.PlaySE("clock_timer");
        }
    }
}