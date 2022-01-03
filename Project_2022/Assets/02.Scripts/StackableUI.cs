using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class StackableUI : MonoBehaviour
{
    public Action _onEnable;
    public Action _onDisable;

    private void OnEnable()
    {
        _onEnable?.Invoke();
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector3.one, 0.3f);
        UIStackManager.AddUIToStack(this);
    }

    private void OnDisable()
    {
        _onDisable?.Invoke();
    }
}
