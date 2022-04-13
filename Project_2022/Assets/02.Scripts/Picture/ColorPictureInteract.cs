using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPictureInteract : MonoBehaviour , IInteractableItem
{
    public int buttonCount;

    [SerializeField] GameObject mainCam = null;
    [SerializeField] GameObject pictureCam = null;
    [SerializeField] GameObject pictureBtnPanel = null;
    [SerializeField] GameObject mainPicture = null;

    [SerializeField] PictureAnswerChecker pictureAnswerChecker;

    BoxCollider boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Interact(GameObject taker)
    {
        pictureCam.SetActive(true);
        pictureAnswerChecker.pictureCam = this.pictureCam;
        boxCollider.enabled = false;
        StartCoroutine(PictureInteract());
    }

    IEnumerator PictureInteract()
    {

        while (Vector3.Distance(pictureCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        pictureBtnPanel.SetActive(true);

        UIManager._instance.DisplayCursor(true);
        for (int i = 0; i < buttonCount; i++)
        {
            pictureBtnPanel.transform.GetChild(i).gameObject.SetActive(true);
            pictureBtnPanel.transform.GetChild(i).GetComponent<ColorChangeBtn>().images.
                Add(mainPicture.transform.GetChild(i).GetComponent<Image>());
        }
    }
}
