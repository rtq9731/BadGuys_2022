using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter3StartDialog : MonoBehaviour
{
    [SerializeField]
    public DialogDatas dialog;

    private void Start()
    {
        DialogManager.Instance.SetDialogData(dialog.GetDialogs());
    }
}
