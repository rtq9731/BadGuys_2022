using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EmailDatas emailDatas;
    public EmailJsonData jsonData = new EmailJsonData();

    readonly string fileName = "Emailes.sav";

    public static GameManager _instance = null;

    public bool _isPaused = false;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;

        emailDatas = Resources.Load<EmailDatas>("EmailDatasSO");
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

    public EmailJsonData LoadEmailData()
    {
        using(StreamReader sr = new StreamReader(Application.persistentDataPath + fileName))
        {
            Debug.Log(Application.persistentDataPath + fileName);
            return jsonData = JsonUtility.FromJson<EmailJsonData>(sr.ReadToEnd());
        }
    }
}
