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

    public GameObject puzzleUI;

    public DialogDatas dialog;

    [SerializeField]
    private int answerDeg;
    public float tryDur;
    public float resetDur;
    private bool isClear;
    private bool isReset;
    private bool isUpDis;
    private bool isDialog;

    private void Awake()
    {
        isDialog = false;
        puzzleCam.SetActive(false);
        puzzleObj.SetActive(false);
        puzzleUI.SetActive(false);
    }

    public void PuzzleOn()
    {
        UIManager.Instance.OnPuzzleUI();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.IsPause = false;

        isClear = false;
        puzzleCam.SetActive(true);
        puzzleObj.SetActive(true);
        AnswerSetting();
        upsidePin.PuzzleStart();
        isUpDis = true;
        StartCoroutine(UIMoving());
    }

    public void PuzzleOver()
    {
        isClear = true;
        isUpDis = false;
        StartCoroutine(UIMoving());
        upsidePin.PuzzleOver();
        puzzleCam.SetActive(false);
        puzzleObj.SetActive(false);

        doorMgr.PuzzleCLear();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
                    Debug.Log("¿­¼è°¡ ´Ù µ¹¾Æ°¬´Ù!");
                    if(!isDialog)
                    {
                        isDialog = true;
                        DialogManager.Instance.SetDialogData(dialog.GetDialogs());
                    }
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

    private IEnumerator UIMoving()
    {
        Vector3 startVector = puzzleUI.transform.localPosition + new Vector3(0, -400, 0);
        Vector3 DesVector = puzzleUI.transform.localPosition;

        if (isUpDis)
        {
            puzzleUI.transform.localPosition = startVector;
            puzzleUI.SetActive(true);
            puzzleUI.transform.DOLocalMove(DesVector, 1f);
        }
        else
        {
            puzzleUI.transform.DOLocalMove(startVector, 1f);
            yield return new WaitForSeconds(1f);
            puzzleUI.SetActive(false);
        }

        yield return null;
    }
}
