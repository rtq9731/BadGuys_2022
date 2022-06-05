using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionTrigger : MonoBehaviour
{
    public System.Action onTrigger = null;

    public bool isTriggered = false;

    private void Awake()
    {
       onTrigger += () => isTriggered = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger?.Invoke();
        }
    }
}
