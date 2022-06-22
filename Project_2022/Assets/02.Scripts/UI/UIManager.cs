using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public StopMenu _stopMenu = null;
    [SerializeField] GameObject _mainUI = null;
    public Action<bool> _onCutSceneChanged = (_isCutScene) => { };

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject().AddComponent<UIManager>();
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
        _onCutSceneChanged(isOnCutScene);
        SoundManager.Instance.PauseAllSound();
    }

    public void OnCutSceneWithoutPause()
    {
        _mainUI.SetActive(false);
        FindObjectOfType<PlayerController>(true).enabled = false;
        isOnCutScene = true;
        _onCutSceneChanged(isOnCutScene);
        SoundManager.Instance.PauseAllSound();
    }

    public void OnCutSceneWithMainUI()
    {
        FindObjectOfType<PlayerController>(true).enabled = false;
        GameManager.Instance.IsPause = true;
        isOnCutScene = true;
        _onCutSceneChanged(isOnCutScene);
        SoundManager.Instance.PauseAllSound();
    }

    public void OnCutSceneOver()
    {
        _mainUI.SetActive(true);
        GameManager.Instance.IsPause = false;
        FindObjectOfType<DialogManager>()?.ClearALLDialog();
        isOnCutScene = false;
        _onCutSceneChanged(isOnCutScene);
        FindObjectOfType<PlayerController>(true).enabled = true;
        SoundManager.Instance.ResumeAllSound();

    }

    public void OnCutSceneOverWithoutClearDialog()
    {
        _mainUI.SetActive(true);
        GameManager.Instance.IsPause = false;
        isOnCutScene = false;
        _onCutSceneChanged(isOnCutScene);
        FindObjectOfType<PlayerController>(true).enabled = true;
        SoundManager.Instance.ResumeAllSound();
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
}
