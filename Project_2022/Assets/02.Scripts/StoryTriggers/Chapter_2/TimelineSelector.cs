using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineSelector : MonoBehaviour
{
    [SerializeField] PlayableDirector[] timelines = null;

    System.Action act = null;

    public void SetTimelineSelector(System.Action onComplete)
    {
        act = onComplete;
    }

    public void PlayTimeline(int idx, bool isEnd)
    {
        timelines[idx].Play();

        if(isEnd)
        {
            act?.Invoke();
            act = null;
        }
    }
}
