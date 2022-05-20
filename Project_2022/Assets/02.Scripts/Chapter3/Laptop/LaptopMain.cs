using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopMain : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject documentPanel;

    private bool grabUpside;

    private void Start()
    {
        grabUpside = false;
    }

    private void Update()
    {
        if(grabUpside && Input.GetMouseButtonDown(0))
        {
            Vector3 distance = transform.position - Input.mousePosition;
            transform.position = Input.mousePosition + distance;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            grabUpside = false;
        }
    }

    public void MainPanelSetting()
    {
        documentPanel.SetActive(false);
    }

    public void DocumentBtn()
    {
        documentPanel.SetActive(true);
    }

    public void DocumentBtnClose()
    {
        documentPanel.SetActive(false);
    }

    //public void UpsideBar()
    //{
    //    grabUpside = true;
    //}
}
