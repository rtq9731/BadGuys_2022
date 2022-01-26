using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailPanel : MonoBehaviour
{
    [SerializeField] Transform btnsParent;
    [SerializeField] EmailInfoPanel infoPanel;
    [SerializeField] EmailButton emailButtonPrefab;

    List<EmailButton> emailBtns = new List<EmailButton>();

    public void OnEnable()
    {
        infoPanel.InitEmailPanel();
        RefreshEmailBtns();
        FindObjectOfType<BottomIcons>()?.RefreshEmailCounter();
    }

    public void RefreshEmailBtns()
    {
        List<EmailData> emails = GameManager._instance.LoadEmailData().emails;
        emails.Sort((x, y) => x.sendTime.CompareTo(y.sendTime));
        for (int i = 0; i < emails.Count; i++)
        {
            EmailButton btn = GetEmailBtn();
            btn.InitEmailButton(emails[i]);
            btn.onClickBtnEmail += infoPanel.InitEmailPanel;
        }
    }

    public void OnDisable()
    {
        emailBtns.ForEach(x => x.gameObject.SetActive(false));
        GameManager._instance.jsonData.emails.ForEach(x => x.isRead = true);
        GameManager._instance.SaveEmailData();
        FindObjectOfType<BottomIcons>()?.RefreshEmailCounter();
    }

    public EmailButton GetEmailBtn()
    {
        EmailButton result = null;

        result = emailBtns.Find(x => !x.gameObject.activeSelf);
        if(result == null)
        {
            result = Instantiate<EmailButton>(emailButtonPrefab, btnsParent);
            emailBtns.Add(result);
        }

        return result;
    }
}
