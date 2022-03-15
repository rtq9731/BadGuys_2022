using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FinaleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject warnPanel;
    [SerializeField]
    private GameObject checkPanel;
    [SerializeField]
    private GameObject stampImg;
    [SerializeField]
    private Button endBtn;
    [SerializeField]
    private InputField freeComent;
    [SerializeField]
    private int camStep;
    [SerializeField]
    private StateMentPanelManager panelManager;
    

    private AudioSource stampSound;

    private void Awake()
    {
        stampSound = GetComponent<AudioSource>();
        freeComent.enabled = false;
        stampImg.SetActive(false);
        checkPanel.SetActive(false);
        endBtn.interactable = false;
    }

    private void Start()
    {
        StateMentOpinionManager.Instance.Cameramoving.AddListener(BtnOnOffFunc);
    }

    private void BtnOnOffFunc(int camStepNum)
    {
        if (camStepNum == camStep)
        {
            endBtn.interactable = true;
            freeComent.enabled = true;
        }
        else
        {
            endBtn.interactable = false;
            freeComent.enabled = false;
        } 
    }

    public void Finish()
    {
        if (StateMentOpinionManager.Instance.isChoose)
        {
            checkPanel.SetActive(true);
            StateMentOpinionManager.Instance.canCameraMove = false;
        }
        else
        {
            warnPanel.SetActive(true);
            StateMentOpinionManager.Instance.canCameraMove = false;
        }
    }

    public void ClsWarn()
    {
        warnPanel.SetActive(false);
        StateMentOpinionManager.Instance.canCameraMove = true;
    }

    public void NotFinish()
    {
        checkPanel.SetActive(false);
        StateMentOpinionManager.Instance.canCameraMove = true;
    }

    public void FinalOkBtn()
    {
        GameManager.Instance.StateMentClear();
        checkPanel.SetActive(false);
        endBtn.interactable = false;
        freeComent.enabled = false;
        StartCoroutine(StampAnim());
    }

    private IEnumerator StampAnim()
    {
        yield return new WaitForSeconds(1f);
        stampSound.Play();
        stampImg.SetActive(true);
        yield return new WaitForSeconds(2f);
        panelManager.ClosePanel();
    }
}
