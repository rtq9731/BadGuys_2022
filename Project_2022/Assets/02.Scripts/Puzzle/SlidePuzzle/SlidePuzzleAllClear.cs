using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlidePuzzleAllClear : MonoBehaviour
{
    public static SlidePuzzleAllClear Instance;
    public Action slideAllClear;

    [SerializeField]
    private int clearCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

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
        slideAllClear?.Invoke();
    }
}
