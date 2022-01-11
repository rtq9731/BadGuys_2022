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
            // 로딩창 및 씬 이동 제어
            LoadingManager.LoadScene("Tutorial");
        });

        _continueBtn.onClick.AddListener(() =>
        {
            // 나중 저장시스템이 나왔을 경우 저장된 게임 데이터 불러오기
            SceneManager.LoadScene(loadSceneName);
        });

        _optionBtn.onClick.AddListener(() =>
        {
            // 옵션창 띄우기
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
