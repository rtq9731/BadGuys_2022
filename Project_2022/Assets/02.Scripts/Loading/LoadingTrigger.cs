using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingTrigger : MonoBehaviour
{
    static LoadingTrigger instance = null;

    public static LoadingTrigger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LoadingTrigger>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Image blindImage = null;

    void DelayFunc()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetString(SceneManager.GetActiveScene().ToString(), "Clear");
        blindImage.DOFade(1, 1.2f).OnComplete(() =>
        {
            StartCoroutine(LoadLobbyStage());
        });
    }

    public void Ontrigger()
    {
        Invoke("DelayFunc", 5f);
    }

    IEnumerator LoadLobbyStage()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("LoadingStageScene");
        async.allowSceneActivation = false;

        float timer = 0f;
        float loadingTime = 1f;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer >= loadingTime)
            {
                SceneManager.LoadScene("MainStage_stage1", LoadSceneMode.Additive);
                async.allowSceneActivation = true;
                yield break;
            }
            yield return null;
        }
    }
}
