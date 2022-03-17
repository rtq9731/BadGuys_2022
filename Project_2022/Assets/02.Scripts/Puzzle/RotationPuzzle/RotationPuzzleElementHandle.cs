using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationPuzzleElementHandle : MonoBehaviour, IInteractableItem
{
    [SerializeField] float rotationAmount = 10f;
    [SerializeField] float rotateDuration = 1f;

    public event Action<float> _onRotate = null;

    Coroutine cor = null;

    float rotationFinish = 0f;

    public void Interact(GameObject taker)
    {
        transform.DOComplete();
        rotationFinish += rotationAmount;
        if(cor == null)
        {
            cor = StartCoroutine(RotateHandle(rotateDuration));
        }
        else
        {
            StopCoroutine(cor);
            cor = StartCoroutine(RotateHandle(rotateDuration));
        }
        //_onRotate?.Invoke(rotationAmount);
    }

    IEnumerator RotateHandle(float duration)
    {
        float timer = 0.001f;
        while (transform.rotation.x <= rotationFinish)
        {
            timer += Time.deltaTime;
            Debug.Log(transform.rotation.x);
            transform.rotation += Quaternion.Euler(new Vector3(Mathf.Lerp(0, rotationFinish, timer / duration), 0, 0));
            yield return null;
        }
    }
}
