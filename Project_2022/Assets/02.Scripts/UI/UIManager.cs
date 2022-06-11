using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public StopMenu _stopMenu = null;
    [SerializeField] GameObject _mainUI = null;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate<GameObject>(null).AddComponent<UIManager>();
                _instance._mainUI = GameObject.Find("MainUIs");
                _instance._stopMenu = FindObjectOfType<StopMenu>(true);
            }

            return _instance;
        }
    }

    private static UIManager _instance = null;

    public GameObject aimPoint;

    public bool isOnCutScene = false;
    public bool isEsc = false;
    public bool isCursor = false;

    private void Awake()
    {
        _instance = this;
        isCursor = false;
        aimPoint = _mainUI.transform.Find("MouseImage").gameObject;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void SetStopMenuDialog(string str, Color color)
    {
        _stopMenu.SetStopDialog(str, color);
    }

    public void OnCutScene()
    {
        _mainUI.SetActive(false);
        FindObjectOfType<PlayerController>(true).enabled = false;
        GameManager.Instance.IsPause = true;
        isOnCutScene = true;
        SoundManager.Instance.StopLoopSound();
    }

    public void OnCutSceneWithoutPause()
    {
        _mainUI.SetActive(false);
        FindObjectOfType<PlayerController>(true).enabled = false;
        isOnCutScene = true;
        SoundManager.Instance.StopLoopSound();
    }

    public void OnCutSceneWithMainUI()
    {
        FindObjectOfType<PlayerController>(true).enabled = false;
        GameManager.Instance.IsPause = true;
        isOnCutScene = true;
        SoundManager.Instance.StopLoopSound();
    }

    public void OnCutSceneOver()
    {
        _mainUI.SetActive(true);
        GameManager.Instance.IsPause = false;
        FindObjectOfType<DialogManager>()?.ClearALLDialog();
        isOnCutScene = false;
        FindObjectOfType<PlayerController>(true).enabled = true;

    }

    public void OnCutSceneOverWithoutClearDialog()
    {
        _mainUI.SetActive(true);
        GameManager.Instance.IsPause = false;
        isOnCutScene = false;
        FindObjectOfType<PlayerController>(true).enabled = true;
    }

    public void OnPuzzleUI()
    {
        aimPoint.SetActive(false);
        isCursor = true;
    }

    public void OffPuzzleUI()
    {
        aimPoint.SetActive(true);
        isCursor = false;
    }

    public void DisplayCursor(bool display)
    {
        Cursor.lockState = display ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = display;
    }

    public void RemoveStopMenu()
    {
        DisplayCursor(true);

        GameManager.Instance.IsPause = false;
        DOTween.PlayAll();

        _mainUI.SetActive(true);
        DialogManager.Instance.gameObject.SetActive(true);

        _stopMenu.gameObject.SetActive(false);
        DisplayCursor(isCursor);

    }

    public void UpdateStopMenu()
    {
        if (_stopMenu.gameObject.activeSelf)
        {
            RemoveStopMenu();
        }
        else
        {
            DisplayStopMenu();
        }
    }

    public void DisplayStopMenu()
    {
        if (UIManager.Instance.isOnCutScene)
            return;

        DisplayCursor(true);
        GameManager.Instance.IsPause = true;
        DOTween.PauseAll();

        _mainUI.SetActive(false);
        DialogManager.Instance.gameObject.SetActive(false);

        _stopMenu.gameObject.SetActive(true);
    }
}
