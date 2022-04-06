using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Triggers;

public class SlidePuzzleAllClear : MonoBehaviour
{
    public static SlidePuzzleAllClear Instance;
    public Action slideAllClear;
    public List<TextAsset> patterns;

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
        //인벤창에 조각넣기
        StageWall.SetActive(false);
        slideAllClear?.Invoke();
    }
}
