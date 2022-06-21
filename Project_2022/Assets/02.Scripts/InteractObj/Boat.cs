using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
                TurnBoat(isSun);
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

        audioSource.SetLoop(true);
        audioSource.Play();
        boatCamera.enabled = true;
        yield return new WaitForSeconds(0.4f);
        SetPaddleAnim(true);

        transform.DOMoveX(230f, 15f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            SetPaddleAnim(false);
            isCanInterct = true;
            isSun = true;
            audioSource.SetLoop(false);
            audioSource.Stop();
        });
    }

    
    //돌리는 함수
    private void TurnBoat(bool isGo)
    {
        if (isGo)
        {
            SetPaddleAnim(true);
            isCanInterct = false;
            isSun = false;
            transform.DORotate(new Vector3(0, -90), 2f).OnComplete(() =>
            {
                float duration = Vector2.Distance(transform.position, originPos);
                audioSource.Play();
                transform.DOMove(originPos, Mathf.Clamp((Mathf.Round(duration / 8)), 5f, 15f)).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    TakeOffBoat();
                    isCanInterct = true;
                    
                });
            });
        }
    }


    // 보트에서 내리는 함수
    private void TakeOffBoat()
    {
        isCanInterct = false;
        blendingCam.GetComponent<BoatCam>().enabled = false;
        transform.DORotate(new Vector3(0, 90), 2f).OnComplete(() =>
        {
            audioSource.Stop();
            isCanInterct = true;
            playerCam.GetComponentInParent<PlayerController>().camTrm = playerCam.transform;
            SetPaddleAnim(false);
            blendingCam.SetActive(false);
            playerCam.GetComponentInParent<PlayerController>().enabled = true;
        });
    }


}
