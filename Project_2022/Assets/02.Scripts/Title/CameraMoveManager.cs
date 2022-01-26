using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraMoveManager : MonoBehaviour
{
    public Transform mainCam = null;

    public Text textReady = null;

    public GameObject vCamTitle = null;
    public GameObject vCamMoniter = null;
    public GameObject vCamLoad = null;

    public GameObject panelEquip = null;

    public CanvasGroup StartCanvasGroup = null;
    public CanvasGroup mainCanvasGroup = null;
    public CanvasGroup loadCanvasGroup = null;
    public CanvasGroup allCanvasGroup = null;
    public CanvasGroup cgVR = null;

    public Image loadingBar = null;

    public Animator vrAnim = null;

    bool isTitle = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        textReady.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        DOTween.CompleteAll();
        StartCanvasGroup.gameObject.SetActive(false);

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

    public void GoToVR()
    {
        StartCoroutine(GoToVRScreen());
    }

    IEnumerator GoToVRScreen()
    {
        panelEquip.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        allCanvasGroup.interactable = false;
        vCamLoad.SetActive(true);
        while(Vector3.Distance(mainCam.position, vCamLoad.transform.position) >= 0.1f)
        {
            yield return null;
        }
        vrAnim.enabled = true;
        yield return new WaitForSeconds(vrAnim.GetCurrentAnimatorClipInfo(0).Length);
        cgVR.DOFade(1, 0.3f).OnComplete(() => LoadingManager.LoadScene("Tutorial", false));
    }
}
