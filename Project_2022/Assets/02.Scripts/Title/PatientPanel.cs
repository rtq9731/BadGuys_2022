using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientPanel : MonoBehaviour
{
    [SerializeField] private PersonInfoList info;
    [SerializeField] private PersonScrollView scrollView;

    private void OnEnable()
    {
        scrollView.MakePatientInfoPanel(info);
    }
}
