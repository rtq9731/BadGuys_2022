using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PatientInfoListSO", menuName = "ScriptableObject/PatientInfoList")]
public class PatientInfoList : ScriptableObject
{
    public List<PatientInfo> patientInfos = new List<PatientInfo>();
}

public class PatientInfo
{
    public int sceneNum = -1;
    public Sprite patientImage = null;
    public string name = "이름 입력되지 않음.";
    public string hurt = "증상 입력되지 않음."; 
}