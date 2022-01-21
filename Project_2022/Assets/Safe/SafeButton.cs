using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeButton : MonoBehaviour, IInteractableItem
{
    public string colorName;
    public SafeManager safeMg;
    private Vector3 firstPos;

    public bool canPush;


    private void Awake()
    {
        canPush = true;
        firstPos = transform.position;
    }

    private void Button_Push()
    {
        safeMg.SafeButton_Push(colorName);
    }

    public void Interact()
    {
        if (canPush)
        {
            transform.localPosition += new Vector3(0, 0, 0.1f);
            canPush = false;
            Debug.LogWarning("´­¸²");

            if (safeMg.buttonCount == 2)
            {
                safeMg.Btn_Unable();
                Invoke("Button_Push", 1f);
            }
            else Button_Push();
        }
    }

    public void BackToNunPush()
    {
        transform.position = firstPos;
        canPush = true;
        Debug.LogWarning("µ¹¾Æ¿È");
    }

    public void DestroySelf()
    {
        Destroy(gameObject.GetComponent<OutlinerOnMouseEnter>());
        Destroy(gameObject.GetComponent<Outline>());
        Destroy(gameObject.GetComponent<SafeButton>());
    }
}
