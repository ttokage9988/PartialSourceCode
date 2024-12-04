using UnityEngine;

public class RotateObj : MonoBehaviour
{

    Vector2 pos; // 最初にクリックしたときの位置
    Quaternion rotation; // 最初にクリックしたときのBoxの角度

    Vector2 vecA; // Boxの中心からposへのベクトル
    Vector2 vecB; // Boxの中心から現在のマウス位置へのベクトル

    float angle; // vecAとvecBが成す角度
    Vector3 AxB; // vecAとvecBの外積

    // PointerDownで呼び出す
    // クリック時にパラメータの初期値を求める
    public void SetPos()
    {
        pos = Camera.main.ScreenToViewportPoint(Input.mousePosition); // マウス位置をワールド座標で取得
        rotation = transform.parent.rotation; // Boxの現在の角度を取得
    }

    // ハンドルをドラッグしている間に呼び出す
    public void Rotate()
    {
        vecA = pos - (Vector2)transform.parent.position; //ある地点からのベクトルを求めるときはこう書くんだった
        vecB = Camera.main.ScreenToViewportPoint(Input.mousePosition) - transform.parent.position; // 上に同じく
        // Vector2にしているのはz座標が悪さをしないようにするためです

        angle = Vector2.Angle(vecA, vecB); // vecAとvecBが成す角度を求める
        AxB = Vector3.Cross(vecA, vecB); // vecAとvecBの外積を求める

        // 外積の z 成分の正負で回転方向を決める
        if (AxB.z > 0)
        {
            transform.parent.localRotation = rotation * Quaternion.Euler(0, 0, angle); // 初期値との掛け算で相対的に回転させる
        }
        else
        {
            transform.parent.localRotation = rotation * Quaternion.Euler(0, 0, -angle); // 初期値との掛け算で相対的に回転させる
        }
    }
}