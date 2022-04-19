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
        _onRotate?.Invoke(rotationAmount);
        if (cor == null)
        {
            cor = StartCoroutine(RotateHandle(rotateDuration));
        }
        else
        {
            StopCoroutine(cor);
            cor = StartCoroutine(RotateHandle(rotateDuration));
        }
    }

    IEnumerator RotateHandle(float duration)
    {
        float timer = 0f;
        while (transform.localRotation.eulerAngles.x != rotationFinish)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Lerp(lastRotation, rotationFinish, timer / duration), 0, 0));
            lastRotation = Mathf.Lerp(lastRotation, rotationFinish, timer / duration);
            yield return null;
        }
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
