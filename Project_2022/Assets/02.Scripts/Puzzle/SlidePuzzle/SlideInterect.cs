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

    private void Awake()
    {
        myCam.SetActive(false);
        slideMnager.clearEvent.AddListener(GameClear_Slide);
    }

    private void Start()
    {
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
        slideMnager.GamePause_Slide();
        CameraOverSetting();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SlidePuzzleAllClear.Instance.AddClearCount();
    }

    private void CameraGameSetting()
    {
        myCam.SetActive(true);
    }

    private void CameraOverSetting()
    {
        myCam.SetActive(false);
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

            slideMnager.enabled = true;
            slideMnager.GameStart_Slide();

            this.enabled = false;
        }
    }
}
