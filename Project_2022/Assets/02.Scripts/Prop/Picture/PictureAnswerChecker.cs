using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Triggers;

public class PictureAnswerChecker : CameraBlending
{
    [SerializeField]
    private PictureInfo[] pictures;

    public ClearParticle clearParticle;
    public PictureInfo pictureInfo;
    public PictureAnswer pictureAnswer;

    public int[] pictureCode;
    public int[] pictureCorrectCode;

    [SerializeField]
    GStageLightTrigger lightTrigger = null;

    [SerializeField]
    GameObject keyPiece;

    bool isCorrect = false;

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
                    blendingCam.SetActive(false);
                    isCorrect = true;
                    StartCoroutine(CameraBlendingCo());
                    AllClearPicture();
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
                    keyPiece.SetActive(true);
                    keyPiece.GetComponent<Item>().Interact(keyPiece);
                    storyWall.GetComponent<WallDissolve>().WallDissolveScene();
                    //storyWall.SetActive(false);
                    lightTrigger.SetActiveGroup(true);
                }
            }
        }
    }
}
