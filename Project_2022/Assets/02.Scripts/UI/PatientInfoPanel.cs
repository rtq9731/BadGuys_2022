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
    PersonInfo info = null;

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

    public void InitPatientInfoPanel(PersonInfo info)
    {
        isInited = true;
        this.info = info;
        patiantImage.sprite = info.personImage;
        textName.text = "요청 기관 : " + info.name;
        textHurt.text = "요구 사항 : " + info.need;
        canStart = GameManager.Instance.IsReadEmail(info.triggerEmailNum);
        offImage.gameObject.SetActive(!canStart);
    }
}
