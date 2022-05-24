using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QTE_CollisionTrigger : MonoBehaviour
{
    [SerializeField] protected float heal = 30f;

    protected QTEManager qm = null;
    protected TimelineSelector selector = null;

    private void Start()
    {
        selector = GetComponent<TimelineSelector>();
        qm = FindObjectOfType<QTEManager>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnTriggered();
        }
    }

    protected abstract void OnTriggered();
}
