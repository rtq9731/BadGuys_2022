using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private float loadingTime = 5f;
    [SerializeField] private Transform loadingIcon = null;

    [SerializeField] private string[] loadingTextsOnEnter;
    [SerializeField] private string[] loadingTextsOnExit;

    [SerializeField] private Text loadingText;
    [SerializeField] private Text loadingPercent;
    [SerializeField] private Text pressAnyBtnText;

    private static string sceneName = "";
    private static bool isExit = false;
    public static string sceneColor = "";
    AsyncOperation operation;

    private void Start()
    {
        DOTween.KillAll();
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

    public static void LoadScene(string sceneName, bool isExit)
    {
        LoadingSceneManager.sceneName = sceneName;
        LoadingSceneManager.isExit = isExit;
        SceneManager.LoadScene("LoadingScene");
    }

    void SetLoadingText(int num)
    {
        if(!isExit)
        {
            loadingText.text = loadingTextsOnEnter[num];
        }   
        else
        {
            loadingText.text = loadingTextsOnExit[num];
        }
    }

    IEnumerator LoadCoroutine(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (timer <= loadingTime)
        {
            timer += Time.deltaTime;

            loadingPercent.text = Mathf.Lerp(0, 100, timer / loadingTime).ToString("##") + "%";
            loadingIcon.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(0, 360, timer / loadingTime)));

            SetLoadingText((int)Mathf.Lerp(0, 5, timer / loadingTime) % loadingTextsOnEnter.Length);

            yield return null;
        }

        loadingText.gameObject.SetActive(false);
        pressAnyBtnText.gameObject.SetActive(true);
    }

    public static IEnumerator LoadStage(string _sceneColor)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("PictureMoveEffectScene");
        async.allowSceneActivation = false;

        sceneColor = _sceneColor;

        float timer = 0f;
        float loadingTime = 3f;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer >= loadingTime)
            {
                SceneManager.LoadScene("MainStage_Stage" + _sceneColor, LoadSceneMode.Additive);
                async.allowSceneActivation = true;
                yield break;
            }
            yield return null;
        }
    }


}
