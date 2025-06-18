using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Aha.Main.UI
{
    /// <summary>
    /// アハのUI管理クラス
    /// </summary>
    class AhaUIManager : MonoBehaviour
    {
        //キャンバス
        [SerializeField] private GameObject mainCanvas;
        [SerializeField] private GameObject resultCanvas;
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] FadeCanvas fadeCanvas;

        //正解UI
        [SerializeField] private GameObject correctPanel;

        public async UniTask CorrectResultRoutine()
        {
            await CorrectEffect();
            OpenResultCanvas();
        }

        private void OpenResultCanvas()
        {
            fadeCanvas.ShowFadePanel(mainCanvas, resultCanvas);
            AudioManager.Instance.PlaySE("correct_voice");
        }

        public void OpenGameOverCanvas()
        {
            gameOverCanvas.SetActive(true);
            AudioManager.Instance.PlaySE("incorrect_voice");
        }

        /// <summary>
        /// 正解演出
        /// </summary>
        private async UniTask CorrectEffect()
        {
            correctPanel.SetActive(true);
            AudioManager.Instance.PlaySE("correct");
            await UniTask.Delay(1200);
            correctPanel.SetActive(false);
        }
    }
}
