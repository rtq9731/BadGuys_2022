using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class QTEManager : MonoBehaviour
{
    [SerializeField] GameObject[] successPlayerableDirector = null;
    [SerializeField] GameObject[] failedPlayerableDirector = null;

    [SerializeField] Chapter2_FinalCutSceneTrigger[] timelineTriggers = null;

    public List<KeyCode> keys = new List<KeyCode>();

    public float time = 0f;

    public bool isSpawnQTE = false;

    Action _successCallback;
    Action _failedCallback;

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
        if (isSpawnQTE)
        {
            time += Time.unscaledDeltaTime;

            if (time >= 10f)
            {
                time = 0;
                isSpawnQTE = false;
                QTEResult(false);

                events.QTEKeys.RemoveAt(0);
                Debug.Log("실패!!");
            }
        }
    }

    //이거로 생성하시면 됩니다
    public void GenerateQTEEvent(QTEPressType qTEPressType, KeyCode key,
        Action successCallback, Action failedCallback)
    {

        _successCallback = successCallback;
        _failedCallback = failedCallback;

        generator.Generation(qTEPressType, key);
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
           
        }

        keys.Clear();
        events.QTEKeys.RemoveAt(0);
    }


    // 결과 처리
    void QTEResult(bool isCorret)
    {
        if (isCorret)
        {
            Debug.Log("맞았음");

            //맞게했을때 행동 
            //맞았을 때 이펙트

            generator.SuccessQTE();

            _successCallback?.Invoke();
            _successCallback = null;

            
        }
        else
        {
            Debug.Log("틀렸음");
            generator.FailQTE();

            //실패했을때 행동
            //실패했을때 이펙트
            generator.FailedQTE();

            _failedCallback?.Invoke();
            _failedCallback = null;

        }

        time = 0f;
        generator.RemoveQTE();
        Time.timeScale = 1f;
    }

    // 입력 처리 
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


                    switch (events.QTEKeys[0].pressType)
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
                    }

                    isSpawnQTE = false;

                    CheckQTEResult();
                }
            }
        }
    }
}