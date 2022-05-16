using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LockPickPuzzle : MonoBehaviour
{
    public DoorLock doorMgr;

    public UpsideLockPick upsidePin;
    public GameObject upsidePinObj;

    public GameObject downsidePin;

    public GameObject keyhole;

    public GameObject puzzleCam;
    public GameObject puzzleObj;

    [SerializeField]
    private int answerDeg;
    public float tryDur;
    public float resetDur;
    private bool isClear;
    private bool isReset;

    private void Awake()
    {
        puzzleCam.SetActive(false);
        puzzleObj.SetActive(false);
    }

    public void PuzzleOn()
    {
        UIManager.Instance.OnCutScene();
        UIManager.Instance.OnPuzzleUI();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        isClear = false;
        puzzleCam.SetActive(true);
        puzzleObj.SetActive(true);
        AnswerSetting();
        upsidePin.PuzzleStart();
    }

    public void PuzzleOver()
    {
        isClear = true;
        upsidePin.PuzzleOver();
        puzzleCam.SetActive(false);
        puzzleObj.SetActive(false);

        doorMgr.PuzzleCLear();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.OnCutSceneOver();
        UIManager.Instance.OffPuzzleUI();
    }

    private void AnswerSetting()
    {
        answerDeg = Random.Range(-85, 86);
        answerDeg = Mathf.Clamp(answerDeg, -85, 85);
        answerDeg += 90;

        Debug.Log(answerDeg);
    }

    private float CalculDeg()
    {
        float degz = upsidePinObj.transform.eulerAngles.z - 270;
        if(degz < 0)
        {
            degz += 360;
        }

        float result = degz - answerDeg;

        Debug.LogWarning(Mathf.Abs(result));
        Debug.LogWarning(degz);
        Debug.LogWarning(answerDeg);

        if (Mathf.Abs(result) <= 5)
        {
            return 180;
        }

        if (Mathf.Abs(result) <= 120)
        {
            return 90 - ( Mathf.Abs(result) * 3 / 4);
        }
        else
        {
            return 0;
        }
    }

    private void Update()
    {
        if (!isClear)
        {
            // 시작하자 마자 끝남
            if (Input.GetKey(KeyCode.E) && !isReset)
            {
                float z = keyhole.transform.eulerAngles.z + (Time.deltaTime / tryDur * 90);
                keyhole.transform.eulerAngles = new Vector3(0, 0, z);

                if (z > CalculDeg())
                {
                    isReset = true;
                    keyhole.transform.DORotate(Vector3.zero, resetDur * 
                        (keyhole.transform.eulerAngles.z / 90));
                }

                if (z >= 90)
                {
                    Debug.Log("열쇠가 다 돌아갔다!");
                    PuzzleOver();
                }
            }
            else
            {
                if (keyhole.transform.eulerAngles.z == 0)
                {
                    isReset = false;
                }
                else
                {
                    isReset = true;
                    keyhole.transform.DORotate(Vector3.zero, resetDur *
                        (keyhole.transform.eulerAngles.z / 90));
                }
            }
        }
    }
}
