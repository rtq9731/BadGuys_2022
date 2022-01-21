using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeButton : MonoBehaviour, IInteractableItem
{
    public string colorName;
    public SafeManager safeMg;
    private Vector3 firstPos;

    private bool canPush;


    private void Awake()
    {
        canPush = true;
        firstPos = transform.position;
    }

    public void Interact()
    {
        if (canPush)
        {
            safeMg.SafeButton_Push(colorName);
            transform.localPosition += new Vector3(0, 0, 0.1f);
            canPush = false;
            Debug.LogWarning("´­¸²");
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
