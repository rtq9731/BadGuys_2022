using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StopMenu : MonoBehaviour
{
    [SerializeField] Button _btnReturn;
    [SerializeField] Button _btnMenu;
    [SerializeField] Button _btnExit;

    public void Start()
    {
        _btnReturn.onClick.AddListener(() =>
        {
            UIManager._instance.DisplayCursor(false);
            GameManager._instance._isPaused = false;
            gameObject.SetActive(false);
        });

        _btnMenu.onClick.AddListener(() =>
        {
            // TODO : ���߿� ���θ޴��� ���ư��� ������
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
}
