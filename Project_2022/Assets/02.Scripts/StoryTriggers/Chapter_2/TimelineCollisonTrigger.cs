using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimelineSelector))]
public class TimelineCollisonTrigger : MonoBehaviour
{
    TimelineSelector selector = null;

    [SerializeField] int timelineIdx;

    private void Awake()
    {
        selector = GetComponent<TimelineSelector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            selector.PlayTimeline(timelineIdx, false);
        }
    }
}
