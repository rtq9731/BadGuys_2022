using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuitiWindowCell : MonoBehaviour, IComparer<MuitiWindowCell>
{
    public int idx = 0;
    public event Action OnDisable = () => {};

    public int Compare(MuitiWindowCell x, MuitiWindowCell y)
    {
        int temp = x.idx;
        switch (x.idx.CompareTo(y.idx))
        {
            case -1:
            case 1:

                x.transform.SetSiblingIndex(y.idx);
                x.idx = y.idx;

                y.transform.SetSiblingIndex(temp);
                y.idx = temp;

                break;
            case 0:
            default:
                break;
        }

        return x.idx.CompareTo(y.idx);
        transform.GetSiblingIndex();
    }
}
