using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OpinionBtnManager : MonoBehaviour
{
    [SerializeField]
    private ParentsBtn[] Btns;
    [SerializeField]
    private int camStep;

    private void Awake()
    {
        camStep = StateMentOpinionManager.Instance.opinionStep;
    }

    private void Start()
    {
        StateMentOpinionManager.Instance.Cameramoving.AddListener(BtnOnOffFunc);
    }

    private void BtnOnOffFunc(int camStepNum)
    {
        if (camStepNum == camStep)
        {
            for (int i = 0; i < Btns.Length; i++)
            {
                Btns[i].GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            for (int i = 0; i < Btns.Length; i++)
            {
                CloseAllParents();
                Btns[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void CloseAllParents()
    {
        StartCoroutine(CAParents());
    }

    IEnumerator CAParents()
    {
        for (int i = 0; i < Btns.Length; i++)
        {
            Btns[i].CloseParentsBtn();
        }

        yield return null;
    }

}
