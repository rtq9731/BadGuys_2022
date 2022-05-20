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
    public GameObject fadeObj;

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
    public bool isUIUse;

    private bool isSetting;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        laptopCam.SetActive(false);
        bootingPanel.SetActive(false);
        loginPanel.SetActive(false);
        mainPanel.SetActive(false);
        isSetting = false;
        isUIUse = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isSetting)
        {
            isSetting = false;
            StartCoroutine(OutScene());
        }
    }

    public void LapTopOn()
    {
        isSetting = false;
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
            StartCoroutine(ReturnMain());
        }
    }

    public void LaptopOff()
    {
        StartCoroutine(OutScene());
    }

    public void MainPanelOn()
    {
        OffAllPanel();
        mainPanel.SetActive(true);
        laptopMain.MainPanelSetting();
        StartCoroutine(FadeOut());
    }

    public void LoginSuccess()
    {
        isSetting = false;
        StartCoroutine(LoginScene());
    }

    private void LoginPanelOn()
    {
        OffAllPanel();
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
        fadeObj.SetActive(true);
        float value = 1;
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
        fadeObj.SetActive(false);
    }
    public IEnumerator FadeIn()
    {
        fadeObj.SetActive(true);
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
        fadeObj.SetActive(false);
    }

    private IEnumerator BootingScene()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        bootingPanel.SetActive(true);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeTime + 2f);
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        bootingPanel.SetActive(false);
        LoginPanelOn();
        yield return new WaitForSeconds(fadeTime);
        isSetting = true;
    }

    private IEnumerator LoginScene()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        OffAllPanel();
        isPass = true;
        MainPanelOn();
        yield return new WaitForSeconds(fadeTime);
        isSetting = true;
    }

    private IEnumerator ReturnMain()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        OffAllPanel();
        MainPanelOn();
        yield return new WaitForSeconds(fadeTime);
        isSetting = true;
    }

    private IEnumerator OutScene()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);
        OffAllPanel();
        laptopCam.SetActive(false);
        UIManager.Instance.OnCutSceneOver();
        UIManager.Instance.OffPuzzleUI();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeTime);
        isUIUse = false;
    }
}
