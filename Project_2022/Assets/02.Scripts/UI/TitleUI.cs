using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] Button _newGameBtn;
    [SerializeField] Button _continueBtn;
    [SerializeField] Button _optionBtn;
    [SerializeField] Button _exitBtn;

    private string loadSceneName = "LoadingScene";

    private void Start()
    {
        _newGameBtn.onClick.AddListener(() =>
        {
            // �ε�â �� �� �̵� ����
            LoadingManager.LoadScene("Tutorial");
        });

        _continueBtn.onClick.AddListener(() =>
        {
            // ���� ����ý����� ������ ��� ����� ���� ������ �ҷ�����
            SceneManager.LoadScene(loadSceneName);
        });

        _optionBtn.onClick.AddListener(() =>
        {
            // �ɼ�â ����
        });

        _exitBtn.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }
}
