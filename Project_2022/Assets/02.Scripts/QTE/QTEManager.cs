using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class QTEManager : MonoBehaviour
{
    [SerializeField] GameObject[] successPlayerableDirector;
    [SerializeField] GameObject[] failedPlayerableDirector;

    public List<KeyCode> keys = new List<KeyCode>();


    public float time = 0f;

    public bool isSpawnQTE = false;

    QTEEvents events;
    QTEGenerator generator;

    int QTEIndex = 0;

    int rollCount = 10;
    private void Start()
    {
        events = GetComponent<QTEEvents>();
        generator = GetComponent<QTEGenerator>();
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
                Debug.Log("실패!!");
            }
        }
    }

    public void GenerateQTEEvent()
    {
        if(events.QTEKeys.Count > 0)
        {
            generator.Generation();
            Time.timeScale = 0.2f;
            isSpawnQTE = true;
        }
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
    }

    void QTEResult(bool isCorret)
    {
        if(isCorret)
        {
            Debug.Log("맞았음");

            //맞게했을때 행동 
            successPlayerableDirector[QTEIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("틀렸음");
            generator.FailQTE();

            //실패했을때 행동
            failedPlayerableDirector[QTEIndex].gameObject.SetActive(true);

        }

        QTEIndex++;
        Debug.Log(QTEIndex);
        time = 0f;
        generator.RemoveQTE(); 
        Time.timeScale = 1f;
    }

    private void OnGUI()
    {
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