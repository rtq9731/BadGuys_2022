using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QTE_CollisionTrigger : MonoBehaviour
{
    [SerializeField] protected float heal = 30f;

    bool isTriggerd = false;

    protected QTEManager qm = null;
    protected TimelineSelector selector = null;

    private void Start()
    {
        selector = GetComponent<TimelineSelector>();
        qm = FindObjectOfType<QTEManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggerd)
            return;

        if(other.CompareTag("Player"))
        {
            isTriggerd = true;
            OnTriggered();
        }
    }

    protected abstract void OnTriggered();
}
