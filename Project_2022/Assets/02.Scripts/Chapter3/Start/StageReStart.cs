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

        restartTxt.DOText("M.A.M : �ý��� ����, ����� �������� ������ϴ�.", 6f).OnComplete(() =>
        {
            restartTxt.text = "";
            restartTxt.DOText("M.A.M : ��� �籸����...", 3f).OnComplete(() =>
            {
                restartTxt.text = "";
                restartTxt.DOText("M.A.M : �籸�� �Ϸ�, ��� �絿��ȭ�� �����մϴ�.", 6f);
            });
        });

        yield return new WaitForSeconds(18f);
        UIManager.Instance.OnCutSceneOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    

}
