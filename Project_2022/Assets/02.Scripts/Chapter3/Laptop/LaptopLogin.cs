using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaptopLogin : MonoBehaviour
{
    public GameObject loginPanel;
    public GameObject loginBtns;
    public GameObject loginWelcome;
    public GameObject loginName;
    public GameObject loginHintBtn;
    public GameObject loginHintTxt;
    public GameObject logWrong;
    public GameObject exitBtn;
    public InputField passwordIPF;
    public Text outputText;

    [SerializeField]
    private int wordCount;
    [SerializeField]
    private string passwordTxt;

    public void LoginPannelSetting()
    {
        loginBtns.SetActive(true);
        loginWelcome.SetActive(false);
        loginName.SetActive(true);
        passwordIPF.onValueChanged.AddListener((word) => WordUpdate(word));
        loginHintBtn.SetActive(true);
        loginHintTxt.SetActive(false);
        logWrong.SetActive(false);
        exitBtn.SetActive(true);

        passwordIPF.text = "";
        passwordTxt = "";
        outputText.text = "";
        wordCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TryLogin();
        }
    }

    private void WordUpdate(string word)
    {
        if (word.Length > wordCount)
        {
            wordCount++;
            passwordTxt = word;
            outputText.text += "??";
        }
        else if (word.Length < wordCount)
        {
            wordCount--;
            passwordTxt = passwordTxt.Substring(0, wordCount);
            outputText.text = outputText.text.Substring(0, wordCount);
        }
    }

    public void Hint_Btn()
    {
        loginHintTxt.SetActive(true);
        loginHintBtn.SetActive(false);
    }

    public void TryLogin()
    {
        string value = passwordIPF.text;
        if (value.CompareTo("" + LaptopManager.Instance.password) == 0)
        {
            LaptopManager.Instance.LoginSuccess();
        }
        else
        {
            passwordIPF.text = "";
            wordCount = 0;
            passwordTxt = "";
            outputText.text = "";
            logWrong.SetActive(true);
        }
    }
}
