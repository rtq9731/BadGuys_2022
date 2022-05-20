using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.Playables.PlayableDirector))]
public class TimelineCollisonTrigger : MonoBehaviour
{
    UnityEngine.Playables.PlayableDirector timelineDirector = null;



    private void Awake()
    {
        timelineDirector = GetComponent<UnityEngine.Playables.PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            timelineDirector.Play();
    }
}
