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
    public string name = "�̸� �Էµ��� ����.";
    public int triggerEmailNum = 0;

    [TextArea]
    public string hurt = "���� �Էµ��� ����."; 
}