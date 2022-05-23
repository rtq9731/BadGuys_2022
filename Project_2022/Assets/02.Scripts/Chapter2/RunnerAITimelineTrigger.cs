using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAITimelineTrigger : MonoBehaviour
{
    UnityEngine.Playables.PlayableDirector timeline = null;
    System.Action act = null;
    private void Awake()
    {
        timeline = GetComponent<UnityEngine.Playables.PlayableDirector>();
    }

    public void SetTrigger(System.Action onComplete)
    {
        act = onComplete;
        timeline.Play();
    }

    public void CompleteEvent()
    {
        act?.Invoke();
    }
}
