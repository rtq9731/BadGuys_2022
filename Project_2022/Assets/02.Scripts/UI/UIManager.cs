using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] StopMenu _stopMenu = null;

    public static UIManager _instance = null;
    private void Awake()
    {
        _instance = this;
    }
    private void OnDestroy()
    {
        _instance = null;
    }

    public void DisplayCursor(bool display)
    {
        Cursor.lockState = display ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = display;
    }

    public void DisplayStopMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager._instance._isPaused = true;
        _stopMenu.gameObject.SetActive(true);
    }
}
