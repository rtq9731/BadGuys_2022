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
    public DialogDatas dialog;

    [Header("Value")]
    public float dialogTime;
    public int password;
    public bool isPass;
    public bool isUIUse;


    private bool isSetting;
    private bool isDialog;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        laptopCam.SetActive(false);
        bootingPanel.SetActive(false);
        loginPanel.SetActive(false);
        mainPanel.SetActive(false);
        isSetting = false;
        isUIUse = false;
        isDialog = false;
    }

    private void Update()
    {
        
    }

    public void CloseLaptop()
    {
        if (!isSetting) return;
        isSetting = false;
        StartCoroutine(OutScene());
    }

    public void LapTopOn()
    {
        isSetting = false;
        UIManager.Instance.OnCutSceneWithMainUI();
        UIManager.Instance.OnPuzzleUI();
        UIManager.Instance.DisplayCursor(true);
        laptopCam.SetActive(true);

        GameManager.Instance.IsPause = false;

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
        if (!isDialog)
        {
            isDialog = true;
            DialogManager.Instance.SetDialogData(dialog.GetDialogs());
        }

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
        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
        UIManager.Instance.OffPuzzleUI();
        UIManager.Instance.DisplayCursor(false);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeTime);
        isUIUse = false;
    }
}
