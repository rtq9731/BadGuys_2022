using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePCUIManager : MonoBehaviour
{
    [SerializeField] Button btnQuit = null;
    [SerializeField] Button btnPerson = null;
    [SerializeField] Button btnInfomation = null;

    private void Awake()
    {
        btnQuit.onClick.AddListener(Application.Quit);
    }
}
