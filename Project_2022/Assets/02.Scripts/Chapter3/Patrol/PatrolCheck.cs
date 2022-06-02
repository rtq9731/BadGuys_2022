 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCheck : MonoBehaviour
{
    public static PatrolCheck Instanse;

    public bool isPlayerIn;
    public bool isDoorClose;

    private void Awake()
    {
        if (Instanse == null)
            Instanse = this;
        else if (Instanse != this)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerIn = false;
    }

    public bool IsHide()
    {
        return (isPlayerIn && isDoorClose);
    }
}
