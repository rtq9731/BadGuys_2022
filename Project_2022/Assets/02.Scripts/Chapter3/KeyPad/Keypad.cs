using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public enum KeyType
{
    Num,
    clear,
    enter,
    del,
    star,
    sharp,
}

public class Keypad : MonoBehaviour
{
    public Text numTxt;
    public Image black;
    public int answer;
    public Animator anim;
    public GameObject timeLineObj;
    public GameObject cam;
    public GameObject[] desThings;
    public SoundScript soundScript;
    public AudioClip[] soundFX;

    private bool isCal;
    private int keyCount;

    private void Awake()
    {
        cam.SetActive(false);
        timeLineObj.SetActive(false);
        keyCount = 0;
        numTxt.text = "";
        isCal = false;

        if (answer == 0) RandAnswer();

        soundScript = gameObject.GetComponent<SoundScript>();
    }

    private void RandAnswer()
    {
        for (int i = 0; i < 4; i++)
        {
            int rand = UnityEngine.Random.Range(1, 10);
            answer += rand * (10 * i + 1);
        }

        Debug.Log(answer);
    }

    private bool CheckAnswer(string value)
    {
        if (("" + answer).CompareTo(value) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PuzzleClear()
    {
        //anim.SetTrigger("OpenTri");
        for (int i = 0; i < desThings.Length; i++)
        {
            Debug.Log("»èÁ¦");
            desThings[i].SetActive(false);
        }
        ClearSoundFX();
        UIManager.Instance.OnCutScene();
        DialogManager.Instance.ClearALLDialog();
        timeLineObj.SetActive(true);
    }

    private void KeyPadSoundFX()
    {
        soundScript.audioSource.clip = soundFX[0];
        soundScript.Play();
    }

    private void ClearSoundFX()
    {
        soundScript.audioSource.clip = soundFX[1];
        soundScript.Play();
    }

    private void WrongSoundFX()
    {
        soundScript.audioSource.clip = soundFX[2];
        soundScript.Play();
    }

    public void DoorOpen()
    {
        anim.SetTrigger("OpenTri");
    }

    public void DoorClose()
    {
        anim.SetTrigger("CloseTri");
    }

    public void ClearScene()
    {
        timeLineObj.SetActive(false);
        black.gameObject.SetActive(true);
        black.color = new Color(0, 0, 0, 1);
        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
        LoadingManager.LoadScene("Credits", true);
    }

    public void KeyInput(KeyType type, string value = "")
    {
        if (isCal) return;

        isCal = true;

        switch (type)
        {
            case KeyType.Num:
                if (keyCount >= 4) break;
                numTxt.text += "" + value;
                KeyPadSoundFX();
                keyCount++;
                break;

            case KeyType.del:
                if (keyCount < 1) break;
                numTxt.text = numTxt.text.Remove(keyCount - 1);
                keyCount--;
                break;

            case KeyType.clear:
                numTxt.text = "";
                keyCount = 0;
                break;

            case KeyType.enter:
                if (keyCount != 4) break;
                if (CheckAnswer(numTxt.text)) PuzzleClear();
                else 
                { 
                    numTxt.text = ""; 
                    keyCount = 0;
                    WrongSoundFX();
                }
                break;

            case KeyType.sharp:
                break;

            case KeyType.star:
                break;

            default:
                break;
        }

        isCal = false;
    }
}
