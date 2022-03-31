using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PiecePuzzleAllClear : MonoBehaviour
{
    public static PiecePuzzleAllClear Instance;
    public Action pieceAllClear;

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
        pieceAllClear?.Invoke();
    }
}
