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
        _instance = this;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    public void OnCutScene()
    {
        _mainUI.SetActive(false);
        GameManager.Instance.IsPause = true;
    }

    public void OnCutSceneOver()
    {
        _mainUI.SetActive(true);
        GameManager.Instance.IsPause = false;
        FindObjectOfType<DialogManager>().ClearALLDialog();
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
        GameManager.Instance.IsPause = true;
        Debug.Log("¤¾¤·");
        _stopMenu.gameObject.SetActive(true);
    }
}
