using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColorPictureInteract : MonoBehaviour , IInteractableItem
{
    public int buttonCount;

    [SerializeField] GameObject mainCam = null;
    [SerializeField] GameObject pictureCam = null;
    [SerializeField] GameObject mainPicture = null;
    [SerializeField] GameObject correctPicture = null;
    [SerializeField] GameObject btnParent = null;

    [SerializeField] PictureAnswerChecker pictureAnswerChecker;
    [SerializeField] PictureBtnManager pictureBtnManager;

    PlayerController playerController;
    ColorChangeBtn[] colorChangeBtns;

    BoxCollider boxCollider;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void Interact(GameObject taker)
    {
        pictureCam.SetActive(true);

        UIManager.Instance.OnPuzzleUI();

        pictureAnswerChecker.blendingCam = this.pictureCam;
        pictureAnswerChecker.pictureInfo = mainPicture.GetComponent<PictureInfo>();
        pictureAnswerChecker.pictureAnswer = correctPicture.GetComponent<PictureAnswer>();

        boxCollider.enabled = false;
        FindObjectOfType<PlayerController>().enabled = false;

        colorChangeBtns = btnParent.GetComponentsInChildren<ColorChangeBtn>();
        for (int i = 0; i <colorChangeBtns.Length; i++)
        {
            colorChangeBtns[i].mesh.enabled = true;
        }

        pictureBtnManager.colorChangeBtns = colorChangeBtns;
        pictureBtnManager.correctPicture = correctPicture.GetComponent<PictureAnswer>();

        StartCoroutine(PictureInteract());
    }

    IEnumerator PictureInteract()
    {
        while (Vector3.Distance(pictureCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        UIManager.Instance.DisplayCursor(true);

        btnParent.transform.DOLocalMoveZ(-11.05f, 1f);
        foreach (var item in colorChangeBtns)
        {
            item.transform.parent.GetChild(1).DOLocalMoveY(0f, 0.5f);
            item.transform.parent.GetChild(1).DOScale(1, 0.5f).OnComplete(() =>
            {
                item.transform.DOScale(1, 0.4f);
                item.transform.DOLocalMoveY(0.1f, 0.4f);
            });
        }
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
