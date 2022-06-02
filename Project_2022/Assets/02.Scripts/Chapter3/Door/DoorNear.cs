using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNear : MonoBehaviour
{
    public DialogDatas dialog;
    public float dialogTime = 3.5f;
    private bool isDialog;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDialog)
        {
            isDialog = true;
            DialogManager.Instance.SetDialaogs(dialog.GetDialogs());
            Invoke("OffMe", dialogTime);
        }
    }

    private void OffMe()
    {
        transform.GetComponent<Collider>().enabled = false;
    }
}
