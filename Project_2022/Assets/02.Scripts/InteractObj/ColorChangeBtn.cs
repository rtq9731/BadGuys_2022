using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColorChangeBtn : MonoBehaviour, IInteractableItem
{
    public PictureAnswerChecker pictureAnswerChecker;

    public Color[] colors;
    public Color curColor;

    public List<Image> images = new List<Image>();

    int colorIndex = -1;

    bool isCorrect;

    public ColorChangeBtn[] colorChangeBtns;

    void Start()
    {
        curColor = colors[0];
    }

    public void Interact(GameObject taker)
    {   
        if(!isCorrect)
        {
            if (colorIndex == colors.Length - 1)
                colorIndex = -1;

            colorIndex++;
            curColor = colors[colorIndex];

            foreach (Image item in images)
            {
                item.color = curColor;
            }

            ButtonPush();
            Debug.Log(curColor);

            pictureAnswerChecker.pictureInfo = images[0].transform.parent.GetComponent<PictureInfo>();
            pictureAnswerChecker.pictureAnswer = images[0].transform.parent.parent.GetComponentInChildren<PictureAnswer>();

            if (!pictureAnswerChecker.CheckAnswer())
                isCorrect = true;
            DestoryOutline();
        }
        
    }
    
    //버튼 눌리는함수
    void ButtonPush()
    {
        transform.DOLocalMoveY(0, 0.1f).OnComplete(() =>
        {
            transform.DOLocalMoveY(0.1f, 0.1f);
        });
    }

    void DestoryOutline()
    {
        if(isCorrect)
        {
            transform.GetComponent<Outline>().enabled = true;
        }
    }
}
