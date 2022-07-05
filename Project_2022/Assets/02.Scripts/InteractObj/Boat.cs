using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Boat : CameraBlending, IInteractableItem, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField]
    private GameObject playerCam;
    [SerializeField]
    ItemInfo item;
    [SerializeField]
    Outline outline;

    [SerializeField]
    SoundScript audioSource;

    [SerializeField]
    Image blindImage;


    [SerializeField]
    Transform sunPosition;
    [SerializeField]
    Transform sunFrontPos;
    [SerializeField]
    Transform dockSideFrontPos;

    private Vector3 originPos;

    private bool isCanInterct = true;
    private bool isSun = false;

    bool isPause = false;

    Animator[] anim;
    BoatCam boatCamera;
    protected override void Start()
    {
        base.Start();
        originPos = transform.position;
        anim = GetComponentsInChildren<Animator>();
        audioSource = GetComponent<SoundScript>();
    }
    private void Update()
    {
        if(blendingCam.activeSelf)
        {
            if (GameManager.Instance.IsPause)
            {
                boatCamera.enabled = false;
                isPause = true;
            }
            else if(!GameManager.Instance.IsPause && isPause)
            {
                boatCamera.enabled = true;
                isPause = false;
            }
        }
    }

    public void Interact(GameObject taker)
    {
        if ((isCanInterct && Inventory.Instance.FindItemInInventory(item)) || isSun)
        {
            outline.enabled = false;
            SoundManager.Instance.PauseFootSound();
            transform.DOKill();

            if (transform.position == originPos)
            {
                StartCoroutine(StartMove());
            }
            else
            {
                StartCoroutine(TurnBoat(isSun));
            }
        }
    }
    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;


        if ((isCanInterct && Inventory.Instance.FindItemInInventory(item)) || isSun)
            return true;
        else 
            return false;
    }
    public void OnPlayerMouseEnter()
    {
        if (!enabled)
            return;

        if ((isCanInterct && Inventory.Instance.FindItemInInventory(item)) || isSun)
            outline.enabled = true;
        else
            outline.enabled = false;
    }

    public void OnPlayerMouseExit()
    {
        if (!enabled)
            return;

        outline.enabled = false;
    }

    void SetPaddleAnim(bool isMove)
    {
        for (int i = 0; i < 2; i++)
        {
            anim[i].SetBool("IsBoat", isMove);
        }
    }

    // 보트 출발하는 함수
    IEnumerator StartMove()
    {
        blendingCam.SetActive(true);
        isCanInterct = false;
        boatCamera = blendingCam.GetComponent<BoatCam>();
        playerCam.GetComponentInParent<PlayerController>().enabled = false;
        boatCamera.enabled = false;

        while (Vector3.Distance(blendingCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        boatCamera.enabled = true;
        blindImage.DOFade(1, 1f).OnComplete(() =>
        {
            transform.position = sunFrontPos.position;
            isCanInterct = true;
            isSun = true;
            blindImage.DOFade(0, 1f);
            transform.DOMove(sunPosition.position, 4f);
        });
    }

    private IEnumerator TurnBoat(bool isGo)
    {
        if (isGo)
        {
            isCanInterct = false;
            isSun = false;
            transform.DORotate(new Vector3(0, -90), 2f).OnComplete(() =>
            {
                blindImage.DOFade(1, 1f);
            });
            
            yield return new WaitForSeconds(3.0f);

            transform.position = dockSideFrontPos.position;
            blindImage.DOFade(0, 1f);
            TakeOffBoat();
        }
    }

    // 보트에서 내리는 함수
    private void TakeOffBoat()
    {
        isCanInterct = false;
        blendingCam.GetComponent<BoatCam>().enabled = false;
        transform.DOMove(originPos, 4f).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 90), 2f).OnComplete(() =>
            {
                audioSource.Stop();
                isCanInterct = true;
                playerCam.GetComponentInParent<PlayerController>().camTrm = playerCam.transform;
                SetPaddleAnim(false);
                blendingCam.SetActive(false);
                StartCoroutine(CameraBlendingCo());
            });
        });
        
    }


}
