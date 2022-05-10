using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShootingTarget : MonoBehaviour
{
    float limitTime = 4f;

    private void Start()
    {
        
    }

    public void FadeTarget()
    {
        transform.DOScale(0, limitTime).OnComplete(() =>
        {
            Debug.Log("½ÇÆÐ!!");
            Destroy(this.gameObject);
        });
    }
}
