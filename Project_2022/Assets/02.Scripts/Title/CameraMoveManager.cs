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

    public Image loadingBar = null;

    bool isTitle = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.anyKeyDown && isTitle)
        {
            GoToMain();
        }
    }

    public void GoToMain()
    {
        isTitle = false;

        mainCanvasGroup.alpha = 0;
        mainCanvasGroup.interactable = false;
        loadCanvasGroup.alpha = 0;
        allCanvasGroup.alpha = 0;
        loadingBar.fillAmount = 0;
        allCanvasGroup.DOFade(1, 1.55f).SetEase(Ease.InExpo).OnComplete(() => 
        {
            loadCanvasGroup.DOFade(1, 2f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                loadingBar.DOFillAmount(1, 5).SetEase(Ease.OutQuart).OnComplete(() =>
                {
                    loadCanvasGroup.alpha = 0;
                    mainCanvasGroup.DOFade(1, 2f);
                    mainCanvasGroup.interactable = true;
                    enabled = false;
                });
            });
        });

        vCamTitle.SetActive(false);
        vCamMoniter.SetActive(true);
    }
}
