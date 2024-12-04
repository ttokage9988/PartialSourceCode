using UnityEngine;

public class TaskCube : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        //ポップアップを表示
        FindObjectOfType<UICanvasManager>().ActiveText(0);

        //Eが押されたら
        if (Input.GetKeyDown(KeyCode.E))
        {
            //たすくキャンバス表示
            GameObject.FindObjectOfType<MainManager>().ActiveTaskCanvas(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //ポップアップを表示
        FindObjectOfType<UICanvasManager>().DisableText();
    }
}
