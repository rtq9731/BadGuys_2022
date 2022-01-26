using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailButton : MonoBehaviour
{
    [SerializeField] Button btnEmail;

    [SerializeField] Text textTitle;
    [SerializeField] Text textSender;

    public event Action<EmailData> onClickBtnEmail = (x) => { };

    EmailData data;

    public void InitEmailButton(EmailData data)
    {
        this.data = data;
        EmailTextData textData = GameManager._instance.GetEmailText(data.textDataID);
        textTitle.text = textData.title;
        textSender.text = textData.sender;
    }

    private void Start()
    {
        btnEmail.onClick.AddListener(OnClickBtnEmail);
    }

    public void OnClickBtnEmail()
    {
        onClickBtnEmail(data);
    }
}
