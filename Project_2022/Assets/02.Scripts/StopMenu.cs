using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StopMenu : MonoBehaviour
{
    [SerializeField] Button _btnDialog = null;
    [SerializeField] Button _btnExit = null;
    [SerializeField] Button _btnOption = null;

    [SerializeField] StopMenuDialogPanel _dialogPanel = null;

    PauseMenuCaller caller = null;

    private void Awake()
    {
        caller = GetComponent<PauseMenuCaller>();
    }

    public void Start()
    {
        _btnOption.onClick.AddListener(() =>
        {
            caller.CallPanelGroup(PauseMenuCaller.PaenlGroupKind.OPTIONSELECTGROUP);
        });

        _btnDialog.onClick.AddListener(() =>
        {
            caller.CallPanelGroup(PauseMenuCaller.PaenlGroupKind.DIALOGGROUP);
        });

        _btnExit.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }

    public void SetStopDialog(string str, Color color)
    {
        _dialogPanel.SetDialog(str, color);
    }

    private void OnEnable()
    {
        UIManager.Instance.isEsc = true;
    }

    private void OnDisable()
    {
        UIManager.Instance.isEsc = false;
    }
}
