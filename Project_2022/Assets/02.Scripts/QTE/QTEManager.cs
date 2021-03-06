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

    Action _successCallback;
    Action _failedCallback;

    QTEEvents events;
    QTEGenerator generator;

    [SerializeField] QTESound _sound;

    int rollCount = 10;
    private void Start()
    {
        events = GetComponent<QTEEvents>();
        generator = GetComponent<QTEGenerator>();
    }

    private void Update()
    {
        if (isSpawnQTE && !GameManager.Instance.IsPause)
        {
            time += Time.unscaledDeltaTime;

            if (time >= 3f)
            {
                time = 0;
                isSpawnQTE = false;
                QTEResult(false);

                events.QTEKeys.RemoveAt(0);
                Debug.Log("????!!");
            }
        }
    }
    public void GenerateQTEEvent(QTEPressType qTEPressType, KeyCode key,
        Action successCallback, Action failedCallback)
    {

        _successCallback = successCallback;
        _failedCallback = failedCallback;

        UIManager.Instance.OnCutSceneWithoutPause();
        UIManager.Instance.isOnCutScene = false;

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

    void SetQTESound()
    {
        _sound.successSound = generator.sound.successSound;
        _sound.failedSound = generator.sound.failedSound;
    }

    // ???? ó??
    void QTEResult(bool isCorret)
    {
        SetQTESound();
        if (isCorret)
        {
            generator.SuccessQTE();

            _successCallback?.Invoke();

            _sound.SuccessQTE();

            _successCallback = null;


        }
        else
        {
            generator.FailQTE();

            _sound.FailedQTE();
            generator.FailedQTE();

            _failedCallback?.Invoke();
            _failedCallback = null;

        }

        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
        time = 0f;
        generator.RemoveQTE();
        Time.timeScale = 1f;
    }

    // ?Է? ó?? 
    private void OnGUI()
    {
        if (isSpawnQTE && !GameManager.Instance.IsPause)
        {
            if (Input.anyKeyDown)
            {
                Event e = Event.current;
                if (e.isKey)
                {
                    if (e.keyCode == KeyCode.None) return;
                    if (e.keyCode == KeyCode.Escape)
                    {
                        Debug.Log("QTEInput");
                        return;
                    }
                    switch (events.QTEKeys[0].pressType)
                    {
                        case QTEPressType.Roll:
                            {
                                keys.Add(e.keyCode);
                                generator.RollBtn();
                                generator.RollImage(rollCount, keys.Count);

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