using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boat : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject boatCam;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject playerCam;

    private RectTransform rectTrm;

    private Vector3 originPos;

    private bool isCanInterct = true;
    private bool isGoToSun = false;

    bool isPause = false;

    Animator[] anim;
    BoatCam boatCamera;
    private void Start()
    {
        originPos = transform.position;
        
        anim = GetComponentsInChildren<Animator>();
    }
    private void Update()
    {
        if(boatCam.activeSelf)
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
        if (isCanInterct)
        {
            transform.DOKill();

            if (transform.position == originPos)
            {
                StartCoroutine(StartMove());
            }
            else
            {
                TurnBoat(isGoToSun);
            }
        }
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
        boatCam.SetActive(true);

        boatCamera = boatCam.GetComponent<BoatCam>();
        playerCam.GetComponentInParent<PlayerController>().enabled = false;
        isGoToSun = true;
        boatCamera.enabled = false;

        while (Vector3.Distance(boatCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        boatCamera.enabled = true;
        yield return new WaitForSeconds(0.4f);
        SetPaddleAnim(true);

        transform.DOMoveX(230f, 15f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            SetPaddleAnim(false);
        });
    }

    // 보트에서 내리는 함수
    private void TakeOffBoat()
    {
        isCanInterct = false;
        boatCam.GetComponent<BoatCam>().enabled = false;
        transform.DORotate(new Vector3(0, 90), 2f).OnComplete(() =>
        {
            isCanInterct = true;
            playerCam.GetComponentInParent<PlayerController>().camTrm = playerCam.transform;
            SetPaddleAnim(false);
            boatCam.SetActive(false);
            playerCam.GetComponentInParent<PlayerController>().enabled = true;
        });
    }

    //돌리는 함수
    private void TurnBoat(bool isGo)
    {

        isCanInterct = false;
        SetPaddleAnim(true);
        if (isGo)
        {
            isGoToSun = false;
            transform.DORotate(new Vector3(0, -90), 2f).OnComplete(() =>
            {
                isCanInterct = true;
                float duration = Vector2.Distance(transform.position, originPos);

                transform.DOMove(originPos, Mathf.Clamp((Mathf.Round(duration / 8)), 5f, 15f)).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    TakeOffBoat();
                });
            });
        }
        else
        {
            isGoToSun = true;
            transform.DORotate(new Vector3(0, 90), 2f).OnComplete(() =>
            {
                isCanInterct = true;
                float duration = Vector2.Distance(transform.position, new Vector2(230f, transform.position.y));

                transform.DOMoveX(230f, Mathf.Clamp((Mathf.Round(duration / 8)), 5f, 15f)).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    SetPaddleAnim(false);
                });
            });
        }
    }
}
