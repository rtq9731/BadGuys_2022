using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StageReStart : MonoBehaviour
{
    public Image fadeImg;
    public Text restartTxt;
    public float fadeTime;


    private void Awake()
    {
        restartTxt.gameObject.SetActive(false);
        restartTxt.text = "";
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Detection();
        }
    }

    public void Detection()
    {
        UIManager.Instance.OnCutScene();
        StartCoroutine(DeathReturn());
    }

    private IEnumerator DeathReturn()
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

        restartTxt.gameObject.SetActive(true);

        restartTxt.DOText("M.A.M : 시스템 오류, 기억의 영역에서 벗어났습니다.", 6f).OnComplete(() =>
        {
            restartTxt.text = "";
            restartTxt.DOText("M.A.M : 기억 재구성중...", 3f).OnComplete(() =>
            {
                restartTxt.text = "";
                restartTxt.DOText("M.A.M : 재구성 완료, 기억 재동기화를 시작합니다.", 6f);
            });
        });

        yield return new WaitForSeconds(18f);
        UIManager.Instance.OnCutSceneOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    

}
