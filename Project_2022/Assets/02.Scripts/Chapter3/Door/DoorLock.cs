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
    private bool isDialog;

    [Header("Components")]
    public Animator anim;
    public ItemInfo key;
    public LockPickPuzzle puzzleMgr;
    public Collider handle;
    public Collider door;
    public GameObject itemSlot;
    public DialogDatas puzzleDialog;
    public DialogDatas tryDialog;


    private void Awake()
    {
        isLock = true;
        isOpen = false;
        isTest = false;
        isDialog = false;
    }

    private void Start()
    {
        puzzleMgr.gameObject.SetActive(false);
        PatrolCheck.Instanse.isDoorClose = true;
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
        bool iskey = Inventory.Instance.MainItem != null ? iskey = CheckMainitem() : iskey = false;
        
        if (iskey || !isLock || isTest)
        {
            
        }
        else
        {
            if (!isDialog)
            {
                isDialog = true;
                DialogManager.Instance.SetDialaogs(tryDialog.GetDialogs());
            }
            return;
        }
        

        if (isLock)
        {
            isPuzzle = true;
            puzzleMgr.gameObject.SetActive(true);
            puzzleMgr.PuzzleOn();
            handle.enabled = false;
            door.enabled = false;

            DialogManager.Instance.SetDialaogs(puzzleDialog.GetDialogs());
        }
        else
        {
            if (isOpen)
            {
                anim.SetTrigger("IsClose");
                isOpen = false;
                PatrolCheck.Instanse.isDoorClose = true;
            }
                
            else
            {
                anim.SetTrigger("IsOpen");
                isOpen = true;
                PatrolCheck.Instanse.isDoorClose = false;
            }
        }
    }

    private bool CheckMainitem()
    {
        if (Inventory.Instance.MainItem == key)
        {
            Slot mainSlot = itemSlot.transform.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();

            if (mainSlot.itemCount >= 2)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanInteract()
    {
        if(isPuzzle)
        {
            return false;
        }
        else
        {
            return true;
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
