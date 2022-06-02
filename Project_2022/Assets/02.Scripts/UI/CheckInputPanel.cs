using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInputPanel : MonoBehaviour
{
    [SerializeField] Button btnOK;
    [SerializeField] Button btnCancel;
    [SerializeField] Text countText = null;

    [SerializeField] float cancelCool = 10f;
    float cancelTimer = 0f;

    int lastTime = 1;

    bool isOn = false;

    System.Action onCancel;
    System.Action onOK;

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
        if(isOn)
        {
            cancelTimer += Time.deltaTime;

            if (cancelCool <= cancelTimer)
            {
                onCancel();
                cancelTimer = 0f;
                lastTime = 1;
                gameObject.SetActive(false);
                isOn = false;
            }

            if (cancelTimer >= lastTime)
            {
                lastTime++;
                countText.text = $"{cancelCool - lastTime}초 후에 원래 설정으로 돌아갑니다.";
            }
        }
    }

    public void InitInputPanel(System.Action onCancel, System.Action onOK)
    {
        countText.text = $"{cancelCool - lastTime}초 후에 원래 설정으로 돌아갑니다.";
        gameObject.SetActive(true);
        isOn = true;


        this.onCancel = onCancel;

        this.onCancel += () =>
        {
            this.onCancel = null;
            this.onOK = null;
        };

        this.onOK = onOK;

        this.onOK += () =>
        {
            this.onCancel = null;
            this.onOK = null;
        };
    }

    public void CheckInput(bool value)
    {
        if (!value)
        {
            onCancel?.Invoke();
        }
        else
        {
            onOK?.Invoke();
        }

        isOn = false;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        onCancel?.Invoke();
    }
}
