using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInputPanel : MonoBehaviour
{
    [SerializeField] Button btnOK;
    [SerializeField] Button btnCancel;

    [SerializeField] float cancelCool = 10f;
    float cancelTimer = 0f;

    System.Action onCancel;

    private void Start()
    {
        btnOK.onClick.AddListener(() => CheckInput(true));
        btnCancel.onClick.AddListener(() => CheckInput(false));
    }

    private void OnEnable()
    {
        cancelTimer = 0f;
    }

    private void Update()
    {
        cancelTimer += Time.deltaTime;
        if (cancelCool <= cancelTimer)
        {
            onCancel();
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void InitInputPanel(System.Action onCancel)
    {
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
