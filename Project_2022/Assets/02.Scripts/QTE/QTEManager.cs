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
            time += Time.deltaTime;

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
        generator.Generation();
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

    //public void CheckMultiQTE()
    //{
    //    if (keys.Count(x => events.QTEKeys[0].QTEKey.Contains(x)) == events.QTEKeys[0].QTEKey.Count)
    //    {
    //        QTEResult(true);
    //    }
    //    else
    //    {
    //        QTEResult(false);
    //    }
    //}

    public void CheckQTEResult()
    {
        if(events.QTEKeys[0].pressType == QTEPressType.Roll)
        {
            //CheckMultiQTE();
        }
        else
        {
            CheckSingleQTE();
        }

        keys.Clear();
        events.QTEKeys.RemoveAt(0);
        Debug.Log("체크");
    }

    void QTEResult(bool isCorret)
    {
        if(isCorret)
        {
            Debug.Log("맞았음");
            time = 0f;
        }
        else
        {
            Debug.Log("틀렸음");
            time = 0f;
        }
        generator.RemoveQTE();
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
                        Debug.Log(keys.Count);
                        Debug.Log(events.QTEKeys[0].QTEKey.Count);
                        if (keys.Count != events.QTEKeys[0].QTEKey.Count)
                        {
                            return;
                        }
                    }

                    keys.Add(e.keyCode);

                    isSpawnQTE = false;

                    CheckQTEResult();
                }
            }
        }
    }
}