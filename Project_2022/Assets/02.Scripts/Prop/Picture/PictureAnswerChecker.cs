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
        
        pictureCode = pictureInfo.GetPictureInfo();
        pictureCorrectCode = pictureAnswer.pieceColorCode;

        if (pictureCode[index] == pictureCorrectCode[index])
        {
            if(pictureCode[index+1] == pictureCorrectCode[index+1])
            {
                if (pictureCode[index + 2] == pictureCorrectCode[index+2])
                {
                    isCorrect = true;
                    return isCorrect;
                }
                isCorrect = false;
                return isCorrect;
            }
            isCorrect = false;
            return isCorrect;
        }
        else
        {
            isCorrect = false;
            return isCorrect;
        }
    }
}
