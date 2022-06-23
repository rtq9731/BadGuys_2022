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
    public bool isOpen;
    private bool isPuzzle;
    private bool isTest;
    private bool isTouch;
    private bool isDialog;

    [SerializeField] SoundScript openDoor;
    [SerializeField] SoundScript closeDoor;


    [Header("Components")]
    public Animator anim;
    public ItemInfo key;
    public LockPickPuzzle puzzleMgr;
    public Collider handle;
    public Collider door;
    public GameObject itemSlot;
    public HairPinCounter hairpinCount;
    public DialogDatas noPreparePuzzleDialog;
    public DialogDatas PreparePuzzleDialog;
    public DialogDatas noTryDialog;
    public DialogDatas oneTryDialog;




    private void Awake()
    {
        isLock = true;
        isOpen = false;
        isTest = false;
        isTouch = false;
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
            hairpinCount.isTouch = true;

            if (hairpinCount.IsSingle() && !isTouch)
            {
                isTouch = true;
                DialogManager.Instance.SetDialogData(oneTryDialog.GetDialogs());
            }
            if (!isTouch)
            {
                isTouch = true;
                DialogManager.Instance.SetDialogData(noTryDialog.GetDialogs());
            }

            return;
        }
        

        if (isLock && !isDialog)
        {
            handle.enabled = false;
            isDialog = true;

            if (isTouch)
            {
                DialogManager.Instance.SetDialogData(noPreparePuzzleDialog.GetDialogs());
            }
            else
            {
                DialogManager.Instance.SetDialogData(PreparePuzzleDialog.GetDialogs());
            }

            StartCoroutine(PuzzleOn());
        }

        if (!isLock)
        {
            if (isOpen)
            {
                anim.SetTrigger("IsClose");
                isOpen = false;
                Invoke("CloseSound", 0.8f);
                PatrolCheck.Instanse.isDoorClose = true;
            }
                
            else
            {
                anim.SetTrigger("IsOpen");
                isOpen = true;
                Invoke("OpenSound", 0.8f);
                PatrolCheck.Instanse.isDoorClose = false;
            }
        }
    }

    void OpenSound()
    {
        openDoor.Play();
    }

    void CloseSound()
    {
        closeDoor.Play();
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
        handle.enabled = true;
        isPuzzle = false;
        isLock = false;
        handle.enabled = true;
        door.enabled = true;
    }

    private IEnumerator PuzzleOn()
    {
        yield return new WaitForSeconds(0);

        isPuzzle = true;
        puzzleMgr.gameObject.SetActive(true);
        puzzleMgr.PuzzleOn();
        handle.enabled = false;
        door.enabled = false;
    }
}
