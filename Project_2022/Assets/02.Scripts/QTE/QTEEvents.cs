using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum QTEPressType
{
    Single,
    Roll,
}

[Serializable]
public class QTEKeys
{
    public List<KeyCode> QTEKey = new List<KeyCode>();
    public QTEPressType pressType;
}

public class QTEEvents : MonoBehaviour
{
    public List<QTEKeys> QTEKeys = new List<QTEKeys>();
}
