using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColorChangeBtn : MonoBehaviour
{
    public PictureAnswerChecker pictureAnswerChecker;
    public PictureBtnManager pictureBtnManager;

    public Color[] colors;
    public Color curColor;

    public List<Image> images = new List<Image>();

    public GameObject mainPicture;

    Button button;

    int colorIndex = -1;

    bool isCorrect;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Interact();
        });

        curColor = colors[0];
        pictureBtnManager = FindObjectOfType<PictureBtnManager>();
    }

    public void Interact()
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

            if (pictureAnswerChecker.CheckAnswer())
            {
                isCorrect = true;
                StartCoroutine(pictureBtnManager.ClearColorPuzzle());
            }
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
}
