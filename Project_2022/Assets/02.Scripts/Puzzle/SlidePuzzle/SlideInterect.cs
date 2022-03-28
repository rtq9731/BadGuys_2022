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

    private void Awake()
    {
        slideMnager.clearEvent.AddListener(GameClear_Slide);
    }

    private void GameSetting_Slide()
    {
        slideMnager.Shuffle();
        slideMnager.enabled = false;
    }

    private void GameClear_Slide()
    {
        gameClear = true;
    }

    public void Interact(GameObject taker)
    {
        if (gameClear)
        {
            return;
        }
        else
        {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
