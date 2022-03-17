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


    private void Start()
    {
        handle._onRotate += OnRotateHandle;
    }

    private void OnRotateHandle(float amount)
    {
        pictureTrm.DOLocalRotate(new Vector3(0, 0, pictureTrm.rotation.z + amount), rotateDuration).OnComplete(()=>
        {
            _onRotationChanged?.Invoke();
        });
    }

    public void DORotateToAnswer()
    {
        pictureTrm.DOLocalRotate(Vector3.zero, 1f);
    }

    public float GetPictureRotationZ()
    {
        return pictureTrm.rotation.z;
    }
}
