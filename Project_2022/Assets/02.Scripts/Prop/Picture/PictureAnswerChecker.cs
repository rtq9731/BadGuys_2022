using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Triggers;

public class PictureAnswerChecker : MonoBehaviour
{
    [SerializeField]
    private PictureInfo[] pictures;

    public PictureInfo pictureInfo;
    public PictureAnswer pictureAnswer;

    public int[] pictureCode;
    public int[] pictureCorrectCode;

    bool isCorrect = false;

    public StoryTrigger storyTrigger;
    public GameObject storyWall;

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
                    pictureInfo.isClear = true;
                    isCorrect = true;
                    return isCorrect;
                }
            }
        }
        return isCorrect;
    }

    public void AllClearPicture()
    {
        int clearPictureIndex = 0;
        for (int i = 0; i < 3; i++)
        {
            if (pictures[i].GetComponent<PictureInfo>().isClear)
            {
                clearPictureIndex++;
                if(clearPictureIndex == 3)
                {
                    storyTrigger.OnTriggered();
                    storyWall.SetActive(false);
                }
            }
        }
    }
}
