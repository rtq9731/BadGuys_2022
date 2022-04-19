using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SlideInterect : MonoBehaviour, IInteractableItem
{
    public bool gameClear;

    [SerializeField]
    private SlidePuzzleManager slideMnager;
    [SerializeField]
    private GameObject myCam;
    [SerializeField]
    private GameObject slideImage;
    [SerializeField]
    private GameObject slideButton;

    private void Start()
    {
        myCam.SetActive(false);
        slideImage.SetActive(false);
        slideButton.SetActive(false);
        slideMnager.clearEvent += GameClear_Slide;
        GameSetting_Slide();
    }

    private void GameSetting_Slide()
    {
        slideMnager.Shuffle();
        slideMnager.GamePause_Slide();
    }

    private void GameClear_Slide()
    {
        gameClear = true;
        slideImage.SetActive(false);
        slideButton.SetActive(false);
        slideMnager.GamePause_Slide();
        CameraOverSetting();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SlidePuzzleAllClear.Instance.AddClearCount();
        Destroy(this);
    }

    private void CameraGameSetting()
    {
        myCam.SetActive(true);
        UIManager._instance.OnCutScene();
    }

    private void CameraOverSetting()
    {
        myCam.SetActive(false);
        UIManager._instance.OnCutSceneOverWithoutClearDialog();
    }

    public void SkipBtnOn()
    {
        slideButton.SetActive(true);
    }

    public void Interact(GameObject taker)
    {
        if (gameClear)
        {
            return;
        }
        else
        {
            CameraGameSetting();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            slideImage.SetActive(true);
            slideMnager.enabled = true;
            slideMnager.GameStart_Slide();

            this.enabled = false;
        }
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
