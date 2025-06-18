using UnityEngine;

namespace Aha.Main
{
    /// <summary>
    /// ゲームクリア管理クラス
    /// </summary>
    public class GameClearManager : MonoBehaviour
    {
        /// <summary>
        /// 問題データ
        /// </summary>
        [SerializeField] private QuestionStorage questionStorage;

        [SerializeField] private GameObject mainCanvas;
        [SerializeField] private GameObject completeResultCanvas;

        [SerializeField] private FadeCanvas fadeCanvas;

        /// <summary>
        /// ゲームをクリアしているか
        /// </summary>
        /// <returns></returns>
        public bool IsGameClear()
        {
            return (SceneController.SelectedStageNumber >= questionStorage.Objects.Count) ? true : false;
        }

        public void OpenCompleteResultCanvas()
        {
            fadeCanvas.ShowFadePanel(mainCanvas, completeResultCanvas);
            AudioManager.Instance.PlaySE("complete_correct_voice");
        }
    }
}
