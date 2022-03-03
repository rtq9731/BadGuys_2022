using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomIcons : MonoBehaviour
{
    [SerializeField] MultiWindowManager multiWindow;
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
            emailUnreadPanelText.text = $"확인하지 않은 \n{GameManager.Instance.jsonData.emails.FindAll(x => !x.isRead).Count} 개의 메세지가 있습니다.";
        }
        else
        {
            emailUnreadPanelText.transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnClickBtnQuit()
    {
        multiWindow.SortWindows(panelQuit.GetComponent<MultiWindowCell>());
        panelQuit.gameObject.SetActive(true);
    }

    private void OnClickBtnCredit()
    {
        multiWindow.SortWindows(panelCredit.GetComponent<MultiWindowCell>());
        panelCredit.gameObject.SetActive(true);
    }

    private void OnClickBtnPerson()
    {
        multiWindow.SortWindows(panelPersons.GetComponent<MultiWindowCell>());
        panelPersons.gameObject.SetActive(true);
    }

    private void OnClickBtnMail()
    {
        multiWindow.SortWindows(panelEmail.GetComponent<MultiWindowCell>());
        panelEmail.gameObject.SetActive(true);
    }
}
