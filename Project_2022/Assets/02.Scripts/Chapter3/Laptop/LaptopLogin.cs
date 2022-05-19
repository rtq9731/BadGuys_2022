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

    public InputField passwordIPF;

    public void LoginPannelSetting()
    {
        loginBtns.SetActive(true);
        loginWelcome.SetActive(false);
        loginName.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

        }
    }
}
