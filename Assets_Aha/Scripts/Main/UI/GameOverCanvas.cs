using UnityEngine;

namespace Aha.Main.UI
{
    /// <summary>
    /// ゲームオーバーキャンバス
    /// </summary>
    public class GameOverCanvas : MonoBehaviour
    {
        /// <summary>
        /// リトライ
        /// </summary>
        public void Retry()
        {
            SceneController.LoadScene(GameScenes.Scenes.Main);
        }

        /// <summary>
        /// ステージへ戻る
        /// </summary>
        public void GoToStageSelect()
        {
            SceneController.LoadScene(GameScenes.Scenes.StageSelect);
        }
    }
}