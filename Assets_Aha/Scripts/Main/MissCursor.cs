using DG.Tweening;
using UnityEngine;

public class MissCursor : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform canvasTransform;
    public RectTransform rectTransform;

    private RectTransform localTransform;
    private void OnEnable()
    {
        localTransform = GetComponent<RectTransform>();

        localTransform.DOPunchPosition(new Vector3(10f, 0, 0), 1f, 5, 1f);
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out var mousePosition);

        rectTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);
    }
}
