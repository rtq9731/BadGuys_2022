using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationPuzzleElement : MonoBehaviour
{
    public Outline outline = null;

    [SerializeField] Transform pictureTrm = null;

    [SerializeField] float rotateDuration = 1f;

    Coroutine cor = null;

    [SerializeField] float rotationFinish = 0f;
    [SerializeField] float lastRotation = 0f;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        lastRotation = pictureTrm.rotation.eulerAngles.z;
        rotationFinish = pictureTrm.rotation.eulerAngles.z;
    }

    public void RotatePicture(float amount)
    {
        rotationFinish += amount;
        if (cor == null)
        {
            cor = StartCoroutine(RotatePicture());
        }
        else
        {
            StopCoroutine(cor);
            cor = StartCoroutine(RotatePicture());
        }
    }

    public void DORotateToAnswer(float rotation)
    {
        if(cor != null)
        {
            StopCoroutine(cor);
        }
        StartCoroutine(RotateToAnswer(rotation));
    }

    public float GetPictureRotationZ()
    {
        return rotationFinish;
    } 

    IEnumerator RotateToAnswer(float destRot)
    {
        float timer = 0f;
        while (pictureTrm.localRotation.z != 0)
        {
            timer += Time.deltaTime;
            pictureTrm.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(pictureTrm.localRotation.eulerAngles.z, destRot, timer / rotateDuration)));
            yield return null;
        }
    }

    IEnumerator RotatePicture()
    {
        float timer = 0f;
        while (pictureTrm.localRotation.eulerAngles.z != rotationFinish)
        {
            timer += Time.deltaTime;
            lastRotation = Mathf.Lerp(lastRotation, rotationFinish, timer / rotateDuration);
            pictureTrm.localRotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Lerp(lastRotation, rotationFinish, timer / rotateDuration)));
            yield return null;
        }
    }
}
