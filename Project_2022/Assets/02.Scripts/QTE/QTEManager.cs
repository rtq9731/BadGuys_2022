using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum QTEPressType
{
    Single,
    Simultaneously
}

[Serializable]
public class QTEKeys
{
    public List<KeyCode> QTEKey = new List<KeyCode>();
    public QTEPressType pressType;
}

public class QTEManager : MonoBehaviour
{
    public QTEKeys QTEKeys;
    public List<KeyCode> keys = new List<KeyCode>();

    public float time = 3f;

    public bool isSpawnQTE = false;


    int QTEIndex = 0;
    private void Start()
    {
        StartCoroutine(GetPressKey());
    }

    void Update()
    {
        if (GameManager.Instance.IsPause) return;

        if (isSpawnQTE)
        {
            if (Input.anyKeyDown)
            {
                
            }
        }
    }

    IEnumerator GetPressKey()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            GenerateQTEEvent();
        }
    }

    public void GenerateQTEEvent()
    {
        isSpawnQTE = true;
    }

    public void CheckQTEResult()
    {
        Debug.Log(QTEKeys.QTEKey[QTEIndex]);
        if (QTEKeys.QTEKey[QTEIndex] == keys[QTEIndex])
        {
            Debug.Log("¸ÂÃã!");
            QTEIndex++;
        }
        else
        {
            QTEIndex = 0;
            Debug.Log("¸ø¸ÂÃã!");
            keys.Clear();
        }
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

                   

                    keys.Add(e.keyCode);
                    isSpawnQTE = false;

                    CheckQTEResult();
                    if (QTEKeys.pressType == QTEPressType.Simultaneously && keys.Count > 1)
                        CheckQTEResult();
                }
            }
        }
    }
}
