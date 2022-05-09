using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorLock : MonoBehaviour, IInteractableItem
{
    [Header("Values")]
    public float time = 1f;

    [Header("Objects")]
    public ItemInfo key;
    public DoorLockPuzzle puzzleMgr;
    public GameObject puzzleObj;

    private bool isLock;
    private bool isOpen;

    private void Awake()
    {
        isLock = true;
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isLock = false;
        }
    }


    public void Interact(GameObject taker)
    {
        if (isLock)
        {

        }
        else
        {
            if (!isOpen)
                transform.DOLocalRotate(new Vector3(0, 90, 0), time);
            else
                transform.DOLocalRotate(new Vector3(0, 0, 0), time);
        }
    }

    public bool CanInteract()
    {
        if (Inventory.Instance.MainItem == key || isLock)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
