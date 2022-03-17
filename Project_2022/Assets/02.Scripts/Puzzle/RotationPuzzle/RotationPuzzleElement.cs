using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationPuzzleElement : MonoBehaviour
{
    [SerializeField] RotationPuzzleElementHandle handle = null;
    [SerializeField] Transform pictureTrm = null;

    [SerializeField] float rotateDuration = 1f;

    public event Action _onRotationChanged = null;

    Coroutine cor = null;

    float rotationFinish = 0f;
    float lastRotation = 0f;

    private void Start()
    {
        handle._onRotate += OnRotateHandle;
        lastRotation = pictureTrm.rotation.eulerAngles.z;
        rotationFinish = pictureTrm.rotation.eulerAngles.z;
    }

    private void OnRotateHandle(float amount)
    {
        rotationFinish += amount;
        if (cor == null)
        {
            cor = StartCoroutine(RotatePicture(rotateDuration));
        }
        else
        {
            lastRotation = pictureTrm.localRotation.eulerAngles.z;
            StopCoroutine(cor);
            cor = StartCoroutine(RotatePicture(rotateDuration));
            _onRotationChanged?.Invoke();
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
        return pictureTrm.rotation.eulerAngles.z;
    } 

    IEnumerator RotateToAnswer()
    {
        float timer = 0.001f;
        while (pictureTrm.localRotation.z != 0)
        {
            timer += Time.deltaTime;
            pictureTrm.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(pictureTrm.localRotation.eulerAngles.z, 0, timer)));
            yield return null;
        }
    }

    IEnumerator RotatePicture(float duration)
    {
        float timer = 0.001f;
        while (pictureTrm.localRotation.eulerAngles.z != rotationFinish)
        {
            timer += Time.deltaTime;
            Debug.Log(Mathf.Lerp(lastRotation, rotationFinish, timer / duration));
            pictureTrm.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(lastRotation, rotationFinish, timer / duration)));
            yield return null;
        }
        lastRotation = pictureTrm.localRotation.eulerAngles.z;

        _onRotationChanged?.Invoke();
    }
}
