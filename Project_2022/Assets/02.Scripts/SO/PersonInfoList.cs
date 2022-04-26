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
    public string name = "이름 입력되지 않음.";
    public int triggerEmailNum = 0;

    [TextArea]
    public string need = "요구 사항 입력되지 않음."; 
}