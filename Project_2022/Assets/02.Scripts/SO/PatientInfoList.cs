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
    public string name = "�̸� �Էµ��� ����.";
    public string hurt = "���� �Էµ��� ����."; 
}