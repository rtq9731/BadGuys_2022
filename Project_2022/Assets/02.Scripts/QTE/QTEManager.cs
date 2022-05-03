using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class QTEManager : MonoBehaviour
{
    public List<KeyCode> keys = new List<KeyCode>();

    public float time = 0f;

    public bool isSpawnQTE = false;

    QTEEvents events;
    QTEGenerator generator;

    int QTEIndex = 0;

    int rollCount = 10;
    private void Start()
    {
        StartCoroutine(GetPressKey());
        events = GetComponent<QTEEvents>();
        generator = GetComponent<QTEGenerator>();
    }
    IEnumerator GetPressKey()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);


            GenerateQTEEvent();
            
        }
    }
    private void Update()
    {
        if(isSpawnQTE)
        {
            time += Time.unscaledDeltaTime;

            if (time >= 3f)
            {
                time = 0;
                isSpawnQTE = false;
                QTEResult(false);
                events.QTEKeys.RemoveAt(0);
                Debug.Log("����!!");
            }
        }
    }

    public void GenerateQTEEvent()
    {
        if(events.QTEKeys.Count > 0)
        { 
        }
        generator.Generation();
        Time.timeScale = 0.2f;
        isSpawnQTE = true;
    }

    public void CheckSingleQTE()
    {
        if (events.QTEKeys[0].QTEKey[0] == keys[0])
        {
            QTEResult(true);
        }
        else
        {
            QTEResult(false);
        }
    }

    public void CheckRollQTE()
    {
        if (keys.Count(x => events.QTEKeys[0].QTEKey.Contains(x)) >= rollCount)
        {
            QTEResult(true);
        }
        else
        {
            QTEResult(false);
        }
    }

    public void CheckQTEResult()
    {
        if(events.QTEKeys[0].pressType == QTEPressType.Roll)
        {
            CheckRollQTE();
        }
        else
        {
            CheckSingleQTE();
        }

        keys.Clear();
        events.QTEKeys.RemoveAt(0);
        Debug.Log("üũ");
    }

    void QTEResult(bool isCorret)
    {
        if(isCorret)
        {
            Debug.Log("�¾���");
            time = 0f;
        }
        else
        {
            Debug.Log("Ʋ����");
            time = 0f;
        }
        generator.RemoveQTE(); 
        Time.timeScale = 1f;
    }

    private void OnGUI()
    {
        if (GameManager.Instance.IsPause) return;

        if (isSpawnQTE)
        {
            if (Input.anyKeyDown)
            {
                Event e = Event.current;
                if (e.isKey)
                {
                    if (e.keyCode == KeyCode.None) return;

                    if (events.QTEKeys[0].pressType == QTEPressType.Roll)
                    {
                        keys.Add(e.keyCode);
                        generator.RollBtn();

                        if (keys[0] != keys[keys.Count - 1])
                            CheckRollQTE();
                        if (keys[0] != events.QTEKeys[0].QTEKey[0])
                            CheckRollQTE();
                        if (keys.Count != rollCount)
                            return;
                    }
                    else
                    {
                        keys.Add(e.keyCode);
                    }

                    generator.RollBtn();

                    isSpawnQTE = false;

                    CheckQTEResult();
                }
            }
        }
    }
}