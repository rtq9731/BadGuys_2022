using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientInfoPanel : MonoBehaviour
{
    [SerializeField] Image patiantImage = null;
    [SerializeField] RectTransform offImage = null;
    [SerializeField] Text textName = null;
    [SerializeField] Text textHurt = null;
    PatientInfo info = null;

    bool isInited = false;
    public bool canStart = false;

    private void OnEnable()
    {
        if(isInited)
        {
            canStart = GameManager.Instance.IsReadEmail(info.triggerEmailNum);
            offImage.gameObject.SetActive(!canStart);
        }
    }

    public void OnPatientSelect()
    {
        GetComponent<PatientTrigger>().OnSelect(info.sceneName);
    }

    public void InitPatientInfoPanel(PatientInfo info)
    {
        isInited = true;
        this.info = info;
        patiantImage.sprite = info.patientImage;
        textName.text = "ȯ�� �̸� : " + info.name;
        textHurt.text = "���� ���� : " + info.hurt;
        canStart = GameManager.Instance.IsReadEmail(info.triggerEmailNum);
        offImage.gameObject.SetActive(!canStart);
    }
}
