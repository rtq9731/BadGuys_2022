using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EamilDatas", fileName = "EmailDatasSO")]
public class EmailDatas : ScriptableObject
{
    public List<EmailTextData> emailList = new List<EmailTextData>();
}

public class EmailJsonData
{
    public List<EmailData> emails = new List<EmailData>();
}


[Serializable]
public class EmailData : ISerializationCallbackReceiver
{
    public int id = -1;
    public bool isRead = false;
    [SerializeField] long timeTick = 0;
    public DateTime sendTime = DateTime.MaxValue; // 2040년 - {현재} 월 - {현재} 일
    public int textDataID = -1;

    public void OnAfterDeserialize()
    {
        sendTime = new DateTime(timeTick);
    }

    public void OnBeforeSerialize()
    {
        timeTick = sendTime.Ticks;
    }
}

[Serializable]
public class EmailTextData
{
    public string title;
    public string sender;
    public string partName;
    [TextArea]
    public string text;
}
