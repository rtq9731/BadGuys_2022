using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PatientInfoListSO", menuName = "ScriptableObject/PatientInfoList")]
public class PatientInfoList : ScriptableObject
{

}

public class PatientInfo
{
    public Sprite patientImage = null;
    public string name = "이름 입력되지 않음.";
    public string hurt = "증상 입력되지 않음."; 
}