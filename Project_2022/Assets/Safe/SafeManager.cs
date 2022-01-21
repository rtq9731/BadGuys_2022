using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafeManager : MonoBehaviour
{
    public int buttonCount;
    private bool rightPassword;

    [SerializeField]
    private SafeButton[] btnOrder;
    [SerializeField]
    private List<SafeButton> btns;
    [SerializeField]
    GameObject safeDoor;

    private void Awake()
    {
        rightPassword = true;
        buttonCount = 0;
    }

    private void ClearSafe()
    {
        Debug.LogWarning("금고 클리어");
        for (int i = 0; i < 5; i++)
        {
            btns[i].DestroySelf();
        }
    }

    public void WrongPush()
    {
        rightPassword = true;
        buttonCount = 0;

        for (int i = 0; i < 5; i++)
        {
            btns[i].BackToNunPush();
        }
    }

    public void SafeButton_Push(string value)
    {
        buttonCount++;

        if (btnOrder[buttonCount - 1].colorName != value)
            rightPassword = false;

        if (buttonCount >= 3)
        {
            if (rightPassword)
                ClearSafe();
            else
            {
                WrongPush();
            }
                
        }
    }
}
