using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShootingTarget : MonoBehaviour
{
    public void FadeTarget()
    {
        transform.DOScale(0, 3f).SetUpdate(true).OnComplete(() =>
        {
            Debug.Log("½ÇÆÐ!!");

            FindObjectOfType<QTEShooting>().EndShootingQTE();
            Destroy(this.gameObject);
        });
    }
}
