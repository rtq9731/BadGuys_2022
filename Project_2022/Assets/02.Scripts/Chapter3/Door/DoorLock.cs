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

    [Header("Components")]
    public Animator anim;
    public ItemInfo key;
    public DoorLockPuzzle puzzleMgr;
    public GameObject puzzleObj;


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

        if ( iskey|| !isLock)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
