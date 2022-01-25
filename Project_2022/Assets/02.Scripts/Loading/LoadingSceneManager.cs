using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private float loadingTime = 5f;

    [SerializeField] private Text loadingPercent;
    [SerializeField] private Text pressAnyBtnText;

    private static string sceneName = "";

    AsyncOperation operation;

    private void Start()
    {
        StartCoroutine(LoadCoroutine(sceneName));

        pressAnyBtnText.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void Update()
    {
        if(Input.anyKeyDown && pressAnyBtnText.gameObject.activeSelf)
        {
            DOTween.KillAll();
            operation.allowSceneActivation = true;
        }
    }

    public static void LoadScene(string sceneName)
    {
        LoadingSceneManager.sceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (timer <= loadingTime)
        {
            timer += Time.deltaTime;
            loadingPercent.text = Mathf.Lerp(0f, 100f, timer / loadingTime) + "%";
            yield return null;
        }
        pressAnyBtnText.gameObject.SetActive(true);
    }
}
