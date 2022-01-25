using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailJsonData
{
    public List<EmailData> emails = new List<EmailData>();
}


[Serializable]
public class EmailData
{
    public int id;
    public bool isRead;
    public System.DateTime sendTime; // 2040년 - {현재} 월 - {현재} 일
    public int textDataID;
}

[CreateAssetMenu(menuName = "ScriptableObject/EamilDatas", fileName = "EmailDatasSO")]
public class EmailDatas : ScriptableObject
{
    public List<EmailTextData> emailList = new List<EmailTextData>();
}

[Serializable]
public class EmailTextData
{
    public string title;
    public string sender;
    public string partName;
    public string text;
}
