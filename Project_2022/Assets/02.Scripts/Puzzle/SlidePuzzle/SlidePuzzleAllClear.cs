using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Triggers;

public class SlidePuzzleAllClear : MonoBehaviour
{
    public static SlidePuzzleAllClear Instance;
    public Action slideAllClear;

    [SerializeField]
    private int clearCount;

    [SerializeField]
    GStageLightTrigger lightTrigger = null;

    [SerializeField]
    StoryTrigger completeTrigger = null;

    [SerializeField]
    GameObject StageWall;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        //slideAllClear += completeTrigger.OnTriggered;
        slideAllClear += () => lightTrigger.SetActiveGroup(true);

        clearCount = 0;
    }

    public void AddClearCount()
    {
        clearCount++;
        if (clearCount == 3)
            InvokeClearEvent();
    }

    private void InvokeClearEvent()
    {
        StageWall.SetActive(false);
        slideAllClear?.Invoke();
    }
}
