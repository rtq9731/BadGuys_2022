using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StopMenu : MonoBehaviour
{
    [SerializeField] Button _btnReturn = null;
    [SerializeField] Button _btnTodo = null;
    [SerializeField] Button _btnOption = null;
    [SerializeField] Button _btnExit = null;

    [SerializeField] GameObject _optionPanel = null;
    [SerializeField] GameObject _todoPanel = null;

    public void Start()
    {
        _btnReturn.onClick.AddListener(() =>
        {
            UIStackManager.RemoveUIOnTop();
        });

        _btnOption.onClick.AddListener(() =>
        {
            _optionPanel.gameObject.SetActive(true);
            _todoPanel.gameObject.SetActive(false);
        });

        _btnTodo.onClick.AddListener(() =>
        {
            _todoPanel.gameObject.SetActive(true);
            _optionPanel.gameObject.SetActive(false);
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

    private void OnEnable()
    {
        DOTween.PauseAll();
    }

    private void OnDisable()
    {
        UIManager._instance.DisplayCursor(false);

        if (GameManager._instance != null)
        {
            GameManager._instance._isPaused = false;
        }
        DOTween.PlayAll();
    }
}
