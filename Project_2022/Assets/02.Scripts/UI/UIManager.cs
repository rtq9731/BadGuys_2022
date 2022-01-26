using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] StopMenu _stopMenu = null;
    [SerializeField] GameObject _mainUI = null;

    public static UIManager _instance = null;

    public bool isOnCutScene = false;

    private void Awake()
    {
        if (_instance != null)
        {
            _stopMenu = _instance._stopMenu;
            _mainUI = _instance._mainUI;
            Destroy(_instance.gameObject);
        }
        _instance = this;
    }
    private void OnDestroy()
    {
        _instance = null;
    }

    public void OnCutScene()
    {
        _mainUI.SetActive(false);
    }

    public void OnCutSceneOver()
    {
        _mainUI.SetActive(true);
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
        GameManager.Instance._isPaused = true;
        _stopMenu.gameObject.SetActive(true);
    }
}
