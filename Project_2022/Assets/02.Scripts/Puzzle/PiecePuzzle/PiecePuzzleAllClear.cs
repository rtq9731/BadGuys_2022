using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Triggers;

public class PiecePuzzleAllClear : MonoBehaviour
{
    public static PiecePuzzleAllClear Instance;
    public Action pieceAllClear;

    [SerializeField]
    private int clearCount;

    [SerializeField]
    StoryTrigger completeTrigger = null;

    [SerializeField]
    GStageLightTrigger lightTrigger = null;

    [SerializeField]
    GameObject StageWall;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        pieceAllClear += completeTrigger.OnTriggered;
        pieceAllClear += () => lightTrigger.SetActiveGroup(true);

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
        pieceAllClear?.Invoke();
    }
}
