using UnityEngine;

/// <summary>
/// アハの正解 / 不正解を判定するクラス
/// </summary>
public class AhaAnswer_Test : MonoBehaviour
{
    public Camera MainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {            
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);   
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Aha"))
                {
                    Debug.Log("正解です");
                }
                else if (hit.collider.CompareTag("Answer"))
                {
                    Debug.Log("不正解です");
                }
            }
        }
    }
}
