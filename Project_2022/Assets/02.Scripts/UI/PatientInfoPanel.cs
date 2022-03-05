using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientInfoPanel : MonoBehaviour
{
    [SerializeField] Image patiantImage = null;
    [SerializeField] Text textName = null;
    [SerializeField] Text textHurt = null;

    public void InitPatientInfoPanel(PatientInfo info)
    {
        patiantImage.sprite = info.patientImage;
        textName.text = info.name;
        textHurt.text = info.hurt;
    }
}
