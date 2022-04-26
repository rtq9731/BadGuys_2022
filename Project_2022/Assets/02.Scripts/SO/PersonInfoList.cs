using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PatientInfoListSO", menuName = "ScriptableObject/PatientInfoList")]
public class PersonInfoList : ScriptableObject
{
    public List<PersonInfo> patientInfos = new List<PersonInfo>();
}

[System.Serializable]
public class PersonInfo
{
    public string sceneName = "";
    public Sprite personImage = null;
    public string name = "�̸� �Էµ��� ����.";
    public int triggerEmailNum = 0;

    [TextArea]
    public string need = "�䱸 ���� �Էµ��� ����."; 
}