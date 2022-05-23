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
    public int answer;
    public Animator anim;

    private bool isCal;
    private int keyCount;

    private void Awake()
    {
        keyCount = 0;
        numTxt.text = "";
        isCal = false;

        if (answer == 0) RandAnswer();
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
        anim.SetTrigger("OpenTri");
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