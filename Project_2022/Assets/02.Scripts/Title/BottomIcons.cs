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
    [SerializeField] Button btnState;

    [SerializeField] Text emailUnreadCount;
    [SerializeField] Text emailUnreadPanelText;

    [SerializeField] GameObject panelQuit;
    [SerializeField] GameObject panelCredit;
    [SerializeField] GameObject panelPatient;
    [SerializeField] GameObject panelEmail;
    [SerializeField] GameObject panelState;

    private void Awake()
    {
        btnQuit.onClick.AddListener(OnClickBtnQuit);
        btnCredit.onClick.AddListener(OnClickBtnCredit);
        btnPerson.onClick.AddListener(OnClickBtnPerson);
        btnMail.onClick.AddListener(OnClickBtnMail);
        btnState.onClick.AddListener(OnClickBtnState);
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
        panelQuit.gameObject.SetActive(!panelQuit.gameObject.activeSelf);
    }

    private void OnClickBtnCredit()
    {
        multiWindow.SortWindows(panelCredit.GetComponent<MultiWindowCell>());
        panelCredit.gameObject.SetActive(!panelCredit.gameObject.activeSelf);
    }

    private void OnClickBtnPerson()
    {
        multiWindow.SortWindows(panelPatient.GetComponent<MultiWindowCell>());
        panelPatient.gameObject.SetActive(!panelPatient.gameObject.activeSelf);
    }

    private void OnClickBtnMail()
    {
        multiWindow.SortWindows(panelEmail.GetComponent<MultiWindowCell>());
        panelEmail.gameObject.SetActive(!panelEmail.gameObject.activeSelf);
    }

    private void OnClickBtnState()
    {
        multiWindow.SortWindows(panelState.GetComponent<MultiWindowCell>());
        panelState.gameObject.SetActive(!panelState.gameObject.activeSelf);
    }
}
