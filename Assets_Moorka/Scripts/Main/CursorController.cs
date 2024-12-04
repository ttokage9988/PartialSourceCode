using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Awake()
    {
        CursorDisable();
    }

    public void CursorDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CursorEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
