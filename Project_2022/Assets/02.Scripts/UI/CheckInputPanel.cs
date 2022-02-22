using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInputPanel : MonoBehaviour
{
    [SerializeField] Button btnOK;
    [SerializeField] Button btnCancel;

    System.Action onCancel;

    private void Start()
    {
        btnOK.onClick.AddListener(() => CheckInput(true));
        btnCancel.onClick.AddListener(() => CheckInput(false));
    }

    public void InitInputPanel(System.Action onCancel)
    {
        transform.parent.gameObject.SetActive(false);
        this.onCancel = onCancel;
    }

    public void CheckInput(bool value)
    {
        if (!value)
        {
            onCancel?.Invoke();
        }

        transform.parent.gameObject.SetActive(false);
    }
}
