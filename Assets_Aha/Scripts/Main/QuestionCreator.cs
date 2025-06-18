using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Aha.Main
{
    /// <summary>
    /// 問題を生成するクラス
    /// </summary>
    public class QuestionCreator : MonoBehaviour
    {
        [SerializeField] private QuestionStorage questionStorage;

        public void CreateQuestion()
        {
            var prefab = questionStorage.Objects[SceneController.SelectedStageNumber].prefab;
            Instantiate(prefab);            
        }
    }
}