using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureAnswerChecker : MonoBehaviour
{
    public PictureInfo pictureInfo;
    public PictureAnswer pictureAnswer;

    public int[] pictureCode;
    public int[] pictureCorrectCode;

    bool isCorrect = false;

    public bool CheckAnswer()
    {
        int index = 0;
        isCorrect = false;

        pictureCode = pictureInfo.GetPictureInfo();
        pictureCorrectCode = pictureAnswer.pieceColorCode;

        for (int i = 0; i < pictureCode.Length; i++)
        {
            if(pictureCode[i] == pictureCorrectCode[i])
            {
                index++;
                if (index == pictureCode.Length)
                {
                    isCorrect = true;
                    return isCorrect;
                }
            }
        }
        return isCorrect;
    }
}
