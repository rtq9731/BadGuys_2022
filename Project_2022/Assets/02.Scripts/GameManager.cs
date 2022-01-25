using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    readonly string fileName = "Emailes.sav";

    public static GameManager _instance = null;

    public bool _isPaused = false;
    EmailDatas emailDatas;
    public EmailJsonData jsonData;

    private void Awake()
    {
        _instance = this;
        LoadEmailData();
    }
    private void OnDestroy()
    {
        _instance = null;
    }

    public EmailTextData GetEmailText(int id)
    {
        return emailDatas.emailList[id];
    }

    public void SaveEmailData()
    {
        using(StreamWriter sw = new StreamWriter(Application.persistentDataPath + fileName))
        {
            sw.Write(JsonUtility.ToJson(jsonData));
        }
    }

    public void LoadEmailData()
    {
        using(StreamReader sr = new StreamReader(Application.persistentDataPath + fileName))
        {
            jsonData = JsonUtility.FromJson<EmailJsonData>(sr.ReadToEnd());
        }
    }
}
