using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopManager : MonoBehaviour
{
    static public LaptopManager Instance;

    [Header("Fade")]
    public float fadeTime = 1f;
    public Image fadeImg;

    [Header("Panel")]
    public GameObject bootingPanel;
    public GameObject loginPanel;
    public GameObject mainPanel;

    [Header("LaptopCode")]
    public LaptopLogin laptopLogin;
    public LaptopMain laptopMain;

    [Header("Other")]
    public GameObject laptopCam;

    [Header("Value")]
    public bool isPass;
    public int password;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        laptopCam.SetActive(false);
        bootingPanel.SetActive(false);
        loginPanel.SetActive(false);
        mainPanel.SetActive(false);
    }

    public void LapTopOn()
    {
        UIManager.Instance.OnCutScene();
        UIManager.Instance.OnPuzzleUI();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        laptopCam.SetActive(true);

        if (!isPass)
        {
            StartCoroutine(BootingScene());
        }
        else
        {
            StartCoroutine(FadeIn());
            Invoke("MainPanelOn", fadeTime);
        }
    }

    public void LaptopOff()
    {
        StartCoroutine(OutScene());
    }

    public void MainPanelOn()
    {
        mainPanel.SetActive(true);
        laptopMain.MainPanelSetting();
        StartCoroutine(FadeOut());
    }

    private void LoginPanelOn()
    {
        loginPanel.SetActive(true);
        laptopLogin.LoginPannelSetting();
        StartCoroutine(FadeOut());
    }

    private void OffAllPanel()
    {
        bootingPanel.SetActive(false);
        loginPanel.SetActive(false);
        mainPanel.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        float value = 0;
        float a = 0;
        Color col = fadeImg.color;

        while (true)
        {
            yield return null;

            value -= Time.deltaTime / fadeTime;
            a = Mathf.Clamp(value, 0, 1);
            fadeImg.color = new Color(col.r, col.g, col.b, a);

            if (a == 0) break;
        }
    }
    public IEnumerator FadeIn()
    {
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
    }

    private IEnumerator BootingScene()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        bootingPanel.SetActive(true);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeTime + 2f);
        bootingPanel.SetActive(false);
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        LoginPanelOn();
        yield return new WaitForSeconds(fadeTime);
    }

    private IEnumerator OutScene()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        OffAllPanel();
        laptopCam.SetActive(false);
        yield return new WaitForSeconds(fadeTime);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeTime);
    }
}
