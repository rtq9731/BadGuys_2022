using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePCUIManager : MonoBehaviour
{
    [SerializeField] Button btnQuit = null;
    [SerializeField] Button btnPerson = null;
    [SerializeField] Button btnInfomation = null;

    [SerializeField] Button btnGoTutorial = null;

    private void Awake()
    {
        btnQuit.onClick.AddListener(Application.Quit);
        btnGoTutorial.onClick.AddListener(() =>
        {
            FindObjectOfType<CameraMoveManager>().GoToVR();
            // LoadingManager.LoadScene("Tutorial");
        });
    }
}
