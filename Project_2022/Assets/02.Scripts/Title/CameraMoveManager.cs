using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraMoveManager : MonoBehaviour
{
    public GameObject vCamTitle = null;
    public GameObject vCamMoniter = null;

    public CanvasGroup mainCanvasGroup = null;
    public CanvasGroup loadCanvasGroup = null;
    public CanvasGroup allCanvasGroup = null;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GoToMain();
        }
    }

    public void GoToMain()
    {
        loadCanvasGroup.alpha = 0;
        allCanvasGroup.alpha = 0;
        allCanvasGroup.DOFade(1, 1.55f).SetEase(Ease.InExpo).OnComplete(() => 
        {
            loadCanvasGroup.DOFade(1, 2f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                enabled = false;
            });
        });

        vCamTitle.SetActive(false);
        vCamMoniter.SetActive(true);
    }
}
