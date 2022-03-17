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

    [SerializeField] float rotationFinish = 0f;
    [SerializeField] float lastRotation = 0f;

    public void Interact(GameObject taker)
    {
        rotationFinish += rotationAmount;
        if(cor == null)
        {
            cor = StartCoroutine(RotateHandle(rotateDuration));
        }
        else
        {
            _onRotate?.Invoke(rotationAmount);
            lastRotation = transform.rotation.eulerAngles.x;
            StopCoroutine(cor);
            cor = StartCoroutine(RotateHandle(rotateDuration));
        }
    }

    IEnumerator RotateHandle(float duration)
    {
        float timer = 0.001f;
        while (transform.rotation.x <= rotationFinish)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(lastRotation, rotationFinish, timer / duration), 0, 0));
            yield return null;
        }
        lastRotation = transform.rotation.eulerAngles.x;
        _onRotate?.Invoke(rotationAmount);
    }
}
