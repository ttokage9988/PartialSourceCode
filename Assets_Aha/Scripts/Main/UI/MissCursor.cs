using DG.Tweening;
using UnityEngine;

namespace Aha.Main.UI
{
    /// <summary>
    /// お手付きカーソル
    /// </summary>
    public class MissCursor : MonoBehaviour
    {
        public Canvas canvas;
        public RectTransform canvasTransform;
        public RectTransform rectTransform;

        private RectTransform localTransform;
        private void OnEnable()
        {
            localTransform = GetComponent<RectTransform>();

            //左右に揺する
            localTransform.DOPunchPosition(new Vector3(10f, 0, 0), 1f, 5, 1f);
        }

        private void Update()
        {
            //カーソルの場所に追従表示する
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out var mousePosition);

            rectTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);
        }
    }
}