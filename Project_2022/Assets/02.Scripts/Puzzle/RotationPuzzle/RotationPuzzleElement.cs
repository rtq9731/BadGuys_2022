using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationPuzzleElement : MonoBehaviour
{
    [SerializeField] RotationPuzzleElementHandle[] handles = null;
    [SerializeField] Transform pictureTrm = null;

    [SerializeField] float rotateDuration = 1f;

    public event Action _onRotationChanged = null;

    Coroutine cor = null;

    [SerializeField] float rotationFinish = 0f;
    [SerializeField] float lastRotation = 0f;

    private void Start()
    {
        foreach (var item in handles)
        {
            item._onRotate += OnRotateHandle;
        }
        lastRotation = pictureTrm.rotation.eulerAngles.z;
        rotationFinish = pictureTrm.rotation.eulerAngles.z;
    }

    private void OnRotateHandle(float amount)
    {
        rotationFinish += amount;
        _onRotationChanged?.Invoke();
        if (cor == null)
        {
            cor = StartCoroutine(RotatePicture(rotateDuration));
        }
        else
        {
            StopCoroutine(cor);
            cor = StartCoroutine(RotatePicture(rotateDuration));
        }
    }

    public void DORotateToAnswer()
    {
        if(cor != null)
        {
            StopCoroutine(cor);
        }
        StartCoroutine(RotateToAnswer());
    }

    public float GetPictureRotationZ()
    {
        return rotationFinish;
    } 

    IEnumerator RotateToAnswer()
    {
        float timer = 0f;
        while (pictureTrm.localRotation.z != 0)
        {
            timer += Time.deltaTime;
            pictureTrm.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(pictureTrm.localRotation.eulerAngles.z, 0, timer)));
            yield return null;
        }
    }

    IEnumerator RotatePicture(float duration)
    {
        float timer = 0f;
        while (pictureTrm.localRotation.eulerAngles.z != rotationFinish)
        {
            timer += Time.deltaTime;
            lastRotation = Mathf.Lerp(lastRotation, rotationFinish, timer / duration);
            pictureTrm.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(lastRotation, rotationFinish, timer / duration)));
            yield return null;
        }
    }
}
