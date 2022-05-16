using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorLock : MonoBehaviour, IInteractableItem
{
    [Header("Values")]
    public float time = 1f;

    [SerializeField]
    private bool isLock;
    [SerializeField]
    private bool isOpen;
    private bool isPuzzle;
    private bool isTest;

    [Header("Components")]
    public Animator anim;
    public ItemInfo key;
    public LockPickPuzzle puzzleMgr;
    public Collider handle;
    public Collider door;


    private void Awake()
    {
        isLock = true;
        isOpen = false;
        isTest = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            isTest = true;
        }
    }

    public void Interact(GameObject taker)
    {
        if (isLock)
        {
            isPuzzle = true;
            puzzleMgr.PuzzleOn();
            handle.enabled = false;
            door.enabled = false;
        }
        else
        {
            if (isOpen)
            {
                anim.SetTrigger("IsClose");
                isOpen = false;
            }
                
            else
            {
                anim.SetTrigger("IsOpen");
                isOpen = true;
            }
        }
    }

    public bool CanInteract()
    {
        bool iskey = Inventory.Instance.MainItem != null ? iskey = (Inventory.Instance.MainItem == key) : iskey = false;

        if(isPuzzle)
        {
            return false;
        }

        if ( iskey || !isLock || isTest)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PuzzleCLear()
    {
        isPuzzle = false;
        isLock = false;
        handle.enabled = true;
        door.enabled = true;
    }
}
