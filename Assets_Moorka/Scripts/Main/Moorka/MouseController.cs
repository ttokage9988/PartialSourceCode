using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private Transform character;    //キャラクターをInspectorウィンドウから選択してください
    [SerializeField]
    private Transform pivot;    //キャラクターの中心にある空のオブジェクトを選択してください
    int count = 0;

    void Start()
    {
        //エラーが起きないようにNullだった場合、それぞれ設定
        if (character == null)
            character = transform.parent;
        if (pivot == null)
            pivot = transform;
    }

    //カメラ上下移動の最大、最小角度です。Inspectorウィンドウから設定してください
    [Range(-0.999f, -0.5f)]
    public float maxYAngle = -0.5f;
    [Range(0.5f, 0.999f)]
    public float minYAngle = 0.5f;

    void Update()
    {
        //マウスのX,Y軸がどれほど移動したかを取得します
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        //Y軸を更新します（キャラクターを回転）取得したX軸の変更をキャラクターのY軸に反映します
        character.transform.Rotate(0, X_Rotation, 0);

        //次はY軸の設定です。
        float nowAngle = pivot.transform.localRotation.x;
        count++;

        var quaternion = pivot.transform.rotation;
        var rotationX = quaternion.eulerAngles.x;

        if (count == 3)
        {
            Debug.Log(nowAngle);
        }

        if (-Y_Rotation != 0)
        {
            if (0 < Y_Rotation) //上向き マイナス
            {
                if (minYAngle >= -nowAngle)
                {
                    pivot.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
            else//下向き　プラス
            {
                if (-nowAngle >= maxYAngle)
                {
                    pivot.transform.Rotate(-Y_Rotation, 0, 0);
                }
            }
        }
        if (count >= 3) count = 0;
    }
}