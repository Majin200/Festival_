using UnityEngine;

public class Cursor_Manager : MonoBehaviour
{
    public static Cursor_Manager Cursor_instance;

    public void Awake()
    {
        Cursor_instance = this;
    }

    public void Start()
    {
        on_Cursor_Lock();
    }

    public void on_Cursor_Unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void on_Cursor_Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
