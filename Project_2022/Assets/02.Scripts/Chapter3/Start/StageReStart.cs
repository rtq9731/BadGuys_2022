using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageRestart : MonoBehaviour
{
    public Image fadeImg;
    public Text restartTxt;
    public float fadeTime;

    string alertMsg = "";

    private void Awake()
    {
        restartTxt.gameObject.SetActive(false);
        restartTxt.text = "";
    }

    public void Detection(string alertMsg)
    {
        UIManager.Instance.OnCutScene();
        StartCoroutine(DeathReturn(alertMsg));
    }

    private IEnumerator DeathReturn(string alertMsg)
    {
        fadeImg.gameObject.SetActive(true);
        float value = 0;
        float a = 0;
        Color col = fadeImg.color;

        while (true)
        {
            yield return null;

            value += Time.deltaTime / fadeTime;
            a = Mathf.Clamp(value, 0, 1);
            fadeImg.color = new Color(col.r, col.g, col.b, a);
            Debug.Log(a);

            if (a == 1) break;
        }

        yield return null;

        float allWaitTime = 0f;
        
        restartTxt.gameObject.SetActive(true);

        allWaitTime += "M.A.M : 시스템 오류, 기억의 영역에서 벗어났습니다.".Length * 0.1f + 0.5f;
        allWaitTime += "M.A.M : 기억 재구성중...".Length * 0.1f + 0.5f;
        allWaitTime += "M.A.M : 재구성 완료, 기억 재동기화를 시작합니다.".Length * 0.1f + 0.5f;

        restartTxt.DOText("M.A.M : 시스템 오류, 기억의 영역에서 벗어났습니다.", "M.A.M : 시스템 오류, 기억의 영역에서 벗어났습니다.".Length * 0.1f + 0.5f).OnComplete(() =>
        {
            restartTxt.text = "";
            restartTxt.DOText("M.A.M : 기억 재구성중...", "M.A.M : 기억 재구성중...".Length * 0.1f + 0.5f).OnComplete(() =>
            {
                restartTxt.text = "";
                restartTxt.DOText("M.A.M : 재구성 완료, 기억 재동기화를 시작합니다.", "M.A.M : 재구성 완료, 기억 재동기화를 시작합니다.".Length * 0.1f + 0.5f);
            });
        });

        yield return new WaitForSeconds(allWaitTime);

        float timer = 0f;
        while (timer <= 6f)
        {
            timer += Time.deltaTime;
            restartTxt.text = "M.A.M : 재구성 완료, 기억 재동기화를 시작합니다.";
            for (int i = 0; i < (int)(timer) % 3; i++)
            {
                restartTxt.text += ".";
            }

            yield return null;
        }


        this.alertMsg = alertMsg;

        UIManager.Instance.OnCutSceneOver();
        SceneManager.sceneLoaded += OnNextSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnNextSceneLoaded(Scene curScene, LoadSceneMode mode)
    {
        FindObjectOfType<RestartStartCheckPanel>(true).SetStart(alertMsg);
        SceneManager.sceneLoaded -= OnNextSceneLoaded;
    }

    

}
