using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PatientInfoListSO", menuName = "ScriptableObject/PatientInfoList")]
public class PatientInfoList : ScriptableObject
{
    public List<PatientInfo> patientInfos = new List<PatientInfo>();
}

[System.Serializable]
public class PatientInfo
{
    public string sceneName = "";
    public Sprite patientImage = null;
    public string name = "이름 입력되지 않음.";
    public int triggerEmailNum = 0;

    [TextArea]
    public string hurt = "증상 입력되지 않음."; 
}