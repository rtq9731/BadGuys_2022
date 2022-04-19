using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OpinionBtnManager : MonoBehaviour
{
    [SerializeField]
    private ParentsBtn[] Btns;

    private void Start()
    {
        BtnsOn();
    }   

    private void BtnsOn()
    {
        for (int i = 0; i < Btns.Length; i++)
        {
                Btns[i].GetComponent<Button>().interactable = true;
        }
    }

    public void CloseAllParents()
    {
        StartCoroutine(CAParents());
    }

    public void CheckAllParents()
    {
        foreach (ParentsBtn parent in Btns)
        {
            if (parent.btnText.text == "")
            {
                StateMentOpinionManager.Instance.isChoose = false;
                Debug.LogWarning("��Ȱ��ȭ ��ư ����");
                return;
            }
        }

        StateMentOpinionManager.Instance.isChoose = true;
        Debug.LogWarning("��� ��ư Ȱ��ȭ��");
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
