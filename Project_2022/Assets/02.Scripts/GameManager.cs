using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    EmailDataListSO emailDatas;
    public EmailJsonData jsonData = new EmailJsonData();

    readonly string fileName = "Emailes.sav";

    public bool _isFirst = true;
    private bool _isPause;
    public bool IsPause
    {
        get
        {
            return _isPause;
        }

        set
        {
            _isPause = value;
            _onPauseChanged(_isPause);
        }
    }
    public Action<bool> _onPauseChanged = (_isPaused) => { };

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if(_instance == null)
                {
                    GameObject obj = Instantiate(new GameObject());
                    _instance = obj.AddComponent<GameManager>();
                    _instance.emailDatas = Resources.Load<EmailDataListSO>("EmailDataSOList");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(_instance.gameObject);

        emailDatas = new EmailDataListSO();
        emailDatas = Resources.Load<EmailDataListSO>("EmailDataSOList");
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

    public bool IsReadEmail(int id)
    {
        EmailData data = jsonData.emails.Find(item => item.id == id);
        if (data != null)
        {
            return data.isRead;
        }
        return false;
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
            if(sr == null)
            {
                jsonData = new EmailJsonData();
            }
            else
            {
                JsonUtility.FromJson<EmailJsonData>(sr.ReadToEnd());
            }

            return jsonData;
        }
    }

    public string playerName = "ȫ�浿";
    public bool canState;
    public int stateNum;

    public void GameClear(int num)
    {
        canState = true;
        stateNum = num;
    }

    public void StateMentClear()
    {
        canState = false;
        stateNum = 0;
    }
}
