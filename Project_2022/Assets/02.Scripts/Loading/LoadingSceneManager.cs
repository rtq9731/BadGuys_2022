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

    AsyncOperation operation;

    public void SetLoading(string sceneName)
    {
        StartCoroutine(LoadCoroutine(sceneName));

        sceneChangeBtn.onClick.AddListener(() =>
        {
            Debug.Log("씬 로드");
            operation.allowSceneActivation = true;
        });

        pressAnyBtnText.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

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
        }
        DOTween.KillAll();
    }
}
