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

    [HideInInspector]
    public MeshCollider mesh;


    public bool isInteract = true;


    int colorIndex = -1;

    void Start()
    {
        curColor = colors[0];
        pictureBtnManager = FindObjectOfType<PictureBtnManager>();
        mesh = GetComponent<MeshCollider>();
        mesh.enabled = false;
    }

    //버튼 눌리는함수
    void ButtonPush()
    {
        transform.DOLocalMoveY(0, 0.1f).OnComplete(() =>
        {
            transform.DOLocalMoveY(0.1f, 0.1f);
        });
    }

    public void Interact()
    {
        if (colorIndex == colors.Length - 1)
            colorIndex = -1;

        colorIndex++;
        curColor = colors[colorIndex];

        ButtonPush();

        foreach (Image item in images)
        {
            item.color = curColor;
        }

        if (pictureAnswerChecker.CheckAnswer())
        {
            mesh.enabled = false;
            UIManager._instance.OnCutSceneOverWithoutClearDialog();
            StartCoroutine(pictureBtnManager.ClearColorPuzzle());
        }
        isInteract = false;
    }
}