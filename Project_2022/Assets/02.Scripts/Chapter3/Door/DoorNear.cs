using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNear : MonoBehaviour
{
    public DialogDatas dialog;
    private bool isDialog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDialog)
        {
            isDialog = true;
            DialogManager.Instance.SetDialogData(dialog.GetDialogs());
            OffMe();
        }
    }

    private void OffMe()
    {
        transform.GetComponent<Collider>().enabled = false;
    }
}
