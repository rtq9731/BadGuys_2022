using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StopMenu : MonoBehaviour
{
    [SerializeField] Button _btnReturn;
    [SerializeField] Button _btnTodo;
    [SerializeField] Button _btnOption;
    [SerializeField] Button _btnExit;

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

    private void OnDisable()
    {
        UIManager._instance.DisplayCursor(false);
        GameManager._instance._isPaused = false;
    }
}
