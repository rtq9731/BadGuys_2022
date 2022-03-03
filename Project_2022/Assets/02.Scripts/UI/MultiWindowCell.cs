using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWindowCell : MonoBehaviour, IComparable<MultiWindowCell>
{
    public int sortingNum = 0;
    public event Action OnObjDisable;
    public event Action OnObjEnable;

    private void OnEnable()
    {
        sortingNum = transform.GetSiblingIndex();
        Debug.Log(sortingNum);
        OnObjEnable?.Invoke();
    }

    private void OnDisable()
    {
        OnObjDisable?.Invoke();
    }

    public int CompareTo(MultiWindowCell other)
    {
        int temp = sortingNum;
        switch (sortingNum.CompareTo(other.sortingNum))
        {
            case -1:
            case 1:

                transform.SetSiblingIndex(other.sortingNum);
                sortingNum = other.sortingNum;

                other.transform.SetSiblingIndex(temp);
                other.sortingNum = temp;

                break;
            case 0:
            default:
                break;
        }

        return sortingNum.CompareTo(other.sortingNum);
    }
}
