using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{

    [SerializeField] private Slider loadingSlider;
    [SerializeField] private Button sceneChangeBtn;

    private string sceneName = "Main";

    AsyncOperation operation;

    void Start()
    {
        StartCoroutine(LoadCoroutine());

        sceneChangeBtn.onClick.AddListener(() =>
        {
            Debug.Log("씬 로드");
            operation.allowSceneActivation = true;
        });
    }

    IEnumerator LoadCoroutine()
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
                    loadingSlider.gameObject.SetActive(false);
                    
            }
        }
    }
}
