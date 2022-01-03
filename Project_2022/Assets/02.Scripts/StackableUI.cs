using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackableUI : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(Vector3.one, 0.3f);
        UIStackManager.AddUIToStack(this);
    }
}
