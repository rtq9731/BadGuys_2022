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
    [SerializeField] Button _btnExit = null;

    [SerializeField] PanelOption _optionPanel = null;
    [SerializeField] GameObject _todoPanel = null;

    public void Start()
    {
        _btnReturn.onClick.AddListener(() =>
        {
            UIManager._instance.UpdateStopMenu();
        });

        _btnTodo.onClick.AddListener(() =>
        {
            _todoPanel.gameObject.SetActive(true);
            _optionPanel.gameObject.SetActive(false);
        });

        _optionPanel.onChangePanel += () =>
        {
            _todoPanel.gameObject.SetActive(false);
        };

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
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f); // 일시정지의 순서 때문에 한번 더 해줄 필요가 있음.
    }
}
