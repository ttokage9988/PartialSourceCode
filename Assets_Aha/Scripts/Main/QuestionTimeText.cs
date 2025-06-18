using UnityEngine;

namespace Aha.Main.UI
{
    /// <summary>
    /// 出題中のテキストクラス
    /// </summary>
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class QuestionTimeText : MonoBehaviour
    {

        private TMPro.TextMeshProUGUI text;
        private float elaspedTime = 0f;

        [SerializeField]
        private float _Duration = 0.8f;

        void Awake()
        {
            text = GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void Update()
        {
            elaspedTime += Time.deltaTime;
            int intTime = (int)(elaspedTime / _Duration);
            int tentenCount = intTime % 4;

            text.text = "出題中" + new string('.', tentenCount);
        }

        public void Show()
        {
            elaspedTime = 0f;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}