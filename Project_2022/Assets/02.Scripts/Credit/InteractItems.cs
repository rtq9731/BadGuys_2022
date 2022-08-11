using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItems : MonoBehaviour, IPlayerMouseEnterHandler
{

    public event System.Action OnPlayerMouseEnter = null;

    void IPlayerMouseEnterHandler.OnPlayerMouseEnter()
    {
        OnPlayerMouseEnter?.Invoke();
    }
}
