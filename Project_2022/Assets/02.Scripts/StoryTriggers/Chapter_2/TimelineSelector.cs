using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineSelector : MonoBehaviour
{
    [SerializeField] PlayableDirector[] timelines = null;

    System.Action act = null;

    public void PlayTimeline(int idx, System.Action callBack = null)
    {
        timelines[idx].Play();
        act = callBack;
    }

    public void OnCompelete()
    {
        act?.Invoke();
        act = null;
    }
}
