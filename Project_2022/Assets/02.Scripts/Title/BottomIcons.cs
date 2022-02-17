using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomIcons : MonoBehaviour
{
    [SerializeField] Button btnQuit;
    [SerializeField] Button btnCredit;
    [SerializeField] Button btnPerson;
    [SerializeField] Button btnMail;

    [SerializeField] Text emailUnreadCount;
    [SerializeField] Text emailUnreadPanelText;

    [SerializeField] GameObject panelQuit;
    [SerializeField] GameObject panelCredit;
    [SerializeField] GameObject panelPersons;
    [SerializeField] GameObject panelEmail;

    private void Awake()
    {
        btnQuit.onClick.AddListener(OnClickBtnQuit);
        btnCredit.onClick.AddListener(OnClickBtnCredit);
        btnPerson.onClick.AddListener(OnClickBtnPerson);
        btnMail.onClick.AddListener(OnClickBtnMail);
    }

    private void OnEnable()
    {
        RefreshEmailCounter();
    }

    public void RefreshEmailCounter()
    {
        emailUnreadCount.text = GameManager.Instance.jsonData.emails.FindAll(x => !x.isRead).Count.ToString();
        if(GameManager.Instance.jsonData.emails.FindAll(x => !x.isRead).Count > 0)
        {
            emailUnreadPanelText.transform.parent.gameObject.SetActive(true);
            emailUnreadPanelText.text = $"Ȯ������ ���� \n{GameManager.Instance.jsonData.emails.FindAll(x => !x.isRead).Count} ���� �޼����� �ֽ��ϴ�.";
        }
        else
        {
            emailUnreadPanelText.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnClickBtnQuit()
    {
        panelQuit.gameObject.SetActive(true);
    }

    private void OnClickBtnCredit()
    {
        panelCredit.gameObject.SetActive(true);
    }

    private void OnClickBtnPerson()
    {
        panelPersons.gameObject.SetActive(true);
    }

    private void OnClickBtnMail()
    {
        panelEmail.gameObject.SetActive(true);
    }
}
