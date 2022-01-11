using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingSceneManager : MonoBehaviour
{

    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Button sceneChangeBtn;

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
        while (!operation.isDone)
        {
            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                loadingSlider.value = Mathf.Lerp(operation.progress, 1f, timer);
                if (loadingSlider.value >= operation.progress)
                    timer = 0f;
            }
            else
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value, 1f, timer);
                if (loadingSlider.value >= 0.99f)
                {
                    loadingSlider.gameObject.SetActive(false);
                    pressAnyBtnText.gameObject.SetActive(true);
                }
            }

            yield return null;
        }
        DOTween.KillAll();
    }
}
