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
            UIStackManager.RemoveUIOnTop();
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
        DOTween.PauseAll();
        transform.DOScale(1, 0.5f); // �Ͻ������� ���� ������ �ѹ� �� ���� �ʿ䰡 ����.
    }

    private void OnDisable()
    {
        UIManager._instance.DisplayCursor(false);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsPause = false;
        }
        DOTween.PlayAll();
    }
}
