using UnityEngine;

/// <summary>
/// アハの正解 / 不正解を判定するクラス
/// </summary>
public class AhaAnswer : MonoBehaviour
{
    public bool IsAnswered { get; set; } = false;
    public bool IsCorrect { get; private set; } = false;

    public Camera MainCamera;

    public LayerMask AhaLayerMask;
    public LayerMask WrongLayerMask;
    public LayerMask BlockRaycast;

    public Vector2 ClickedPosition { get; private set; }

    private void Update()
    {
        if (IsAnswered) return;

        if (Input.GetMouseButtonDown(0))
        {            
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, BlockRaycast);
                ClickedPosition = hit.point;
                if (hit.collider != null &&
                    (hit.collider.gameObject.layer == LayerMask.NameToLayer("BlockRaycast")))
                {                    
                    // これ以降の当たり判定をスルー
                    return;
                }
            }
            
            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, AhaLayerMask);
                ClickedPosition = hit.point;
                if (hit.collider != null && hit.collider.CompareTag("Aha"))
                {
                    Debug.Log("正解です");
                    IsAnswered = true;
                    IsCorrect = true;

                    // これ以降の当たり判定をスルー
                    return;
                }

            }

            {
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, WrongLayerMask);
                ClickedPosition = hit.point;
                if (hit.collider != null && hit.collider.CompareTag("Answer"))
                {
                    Debug.Log("不正解です");
                    IsAnswered = true;
                    IsCorrect = false;

                    // これ以降の当たり判定をスルー
                    return;
                }
            }
        }
    }
}
