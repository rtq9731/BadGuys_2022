using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class QTEManager : MonoBehaviour
{
    [SerializeField] GameObject[] successPlayerableDirector = null;
    [SerializeField] GameObject[] failedPlayerableDirector = null;

    [SerializeField] CollisionTimelineTrigger[] timelineTriggers = null;

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

        for (int i = 0; i < timelineTriggers.Length; i++)
        {
            int y = i;
            timelineTriggers[i]._onComplete += () => {
                QTEIndex = y;
                GenerateQTEEvent(); 
            };
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
        switch (events.QTEKeys[0].pressType)
        {
            case QTEPressType.Roll:
                {
                    CheckRollQTE();
                }
                break;
            case QTEPressType.Single:
                {
                    CheckSingleQTE();
                }
                break;
            case QTEPressType.Shoot:
                break;
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
            //맞았을 때 이펙트

            generator.SuccessQTE();

            successPlayerableDirector[QTEIndex].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("틀렸음");
            generator.FailQTE();

            //실패했을때 행동
            //실패했을때 이펙트
            generator.FailedQTE();

            failedPlayerableDirector[QTEIndex].gameObject.SetActive(true);
        }

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


                    switch(events.QTEKeys[0].pressType)
                    {
                        case QTEPressType.Roll:
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
                            break;
                        case QTEPressType.Single:
                            {
                                keys.Add(e.keyCode);
                                generator.RollBtn();
                            }
                            break;
                        case QTEPressType.Shoot:
                            break;
                    }

                    isSpawnQTE = false;

                    CheckQTEResult();
                }
            }
        }
    }
}