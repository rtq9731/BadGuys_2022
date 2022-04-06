using DG.Tweening;
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
        if(_instance == this)
        {
            _instance = null;
        }
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
        FindObjectOfType<DialogManager>()?.ClearALLDialog();
    }

    public void OnCutSceneOverWithoutClearDialog()
    {
        _mainUI.SetActive(true);
        GameManager.Instance.IsPause = false;
    }

    public void DisplayCursor(bool display)
    {
        Cursor.lockState = display ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = display;
    }

    public void RemoveStopMenu()
    {
        DisplayCursor(false);
        GameManager.Instance.IsPause = false;
        DOTween.PlayAll();

        _stopMenu.transform.DOScale(0, 0.5f).OnComplete(() => { _stopMenu.gameObject.SetActive(false); });
    }

    public void UpdateStopMenu()
    {
        if(_stopMenu.gameObject.activeSelf)
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
        DisplayCursor(true);
        GameManager.Instance.IsPause = true;
        DOTween.PauseAll();

        _stopMenu.gameObject.SetActive(true);
    }
}
