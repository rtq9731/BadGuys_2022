using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    [SerializeField] DialogManager dialogManager = null;

    [SerializeField] DialogDatas[] datas = null; 

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.IsPause = dialogManager.gameObject.activeSelf;
            dialogManager.gameObject.SetActive(!dialogManager.gameObject.activeSelf);

            if(dialogManager.gameObject.activeSelf)
            {
                DOTween.PlayAll();
            }
            else
            {
                DOTween.PauseAll();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            dialogManager.SetDialogData(datas[0].GetDialogs());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogManager.SetDialogData(datas[1].GetDialogs());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogManager.SetDialogData(datas[2].GetDialogs());
        }
    }
}
