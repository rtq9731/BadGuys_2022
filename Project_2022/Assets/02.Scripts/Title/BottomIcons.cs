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

    private void OnClickBtnQuit()
    {

    }

    private void OnClickBtnCredit()
    {

    }

    private void OnClickBtnPerson()
    {

    }

    private void OnClickBtnMail()
    {

    }
}
