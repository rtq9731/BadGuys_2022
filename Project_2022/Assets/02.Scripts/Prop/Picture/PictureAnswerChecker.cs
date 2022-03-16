using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureAnswerChecker : MonoBehaviour
{
    public PictureInfo pictureInfo;
    public PictureAnswer pictureAnswer;

    public int[] pictureCode;
    public int[] pictureCorrectCode;

    bool isCorrect;

    public bool CheckAnswer()
    {
        pictureCode = pictureInfo.GetPictureInfo();
        pictureCorrectCode = pictureAnswer.pieceColorCode;
        for (int i = 0; i < pictureCode.Length; i++)
        {
            if(pictureCode[i] == pictureCorrectCode[i])
            {
                isCorrect = true;
                return isCorrect;
            }
            else
            {
                isCorrect = false;
                break;
            }
        }
        return false;
    }
}
