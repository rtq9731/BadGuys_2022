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
    private Image blindImage;

    public void Ontrigger()
    {
        //다이얼로그나 연출을 다 본후에 실행하게
        blindImage.DOFade(1, 1.2f).OnComplete(() =>
        {
            StartCoroutine(LoadLobbyStage());
        });
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
