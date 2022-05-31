using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiWindowCell : MonoBehaviour
{
    public event Action OnObjEnable;

    private void OnEnable()
    {
        OnObjEnable?.Invoke();
    }
}
