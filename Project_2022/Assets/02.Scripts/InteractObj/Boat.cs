using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boat : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject boatCam;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject playerCam;

    private RectTransform rectTrm;

    private Vector3 originPos;

    private bool isCanInterct = true;

    Animator[] anim;

    private void Start()
    {
        rectTrm = inventory.transform.GetChild(0).GetComponent<RectTransform>();
        originPos = transform.position;

        anim = GetComponentsInChildren<Animator>();
    }

    public void Interact(GameObject taker)
    {
        if(isCanInterct)
        {
            if (transform.position == originPos)
            {
                StartCoroutine(StartMove());
            }
            else
            {
                ReturnMove();
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
        inventory.SetActive(false);

        playerCam.GetComponentInParent<PlayerController>().enabled = false;


        //playerCam.GetComponentInParent<PlayerController>().camTrm = boatCam.transform;

        while (Vector3.Distance(boatCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.4f);
        SetPaddleAnim(true);


        //sun obj 바로 앞으로 가게끔 바꾸기
        transform.DOMoveX(230f, 15f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            inventory.SetActive(true);
            SetPaddleAnim(false);
        });
    }

    // 처음 자리로 돌아가는 함수
    private void ReturnMove()
    {
         DOTween.KillAll();

         SetPaddleAnim(true);
         TurnBoat();
         inventory.SetActive(false);
        
    }

    // 보트에서 내리는 함수
    private void TakeOffBoat()
    {
        isCanInterct = false;
        transform.DORotate(new Vector3(0, 90), 2f).OnComplete(() =>
        {
            isCanInterct = true;
            playerCam.GetComponentInParent<PlayerController>().camTrm = playerCam.transform;
            SetPaddleAnim(false);
            boatCam.SetActive(false);
            playerCam.GetComponentInParent<PlayerController>().enabled = true;
        });
    }


    // 보트 돌리는 함수
    private void TurnBoat()
    {
        if(transform.position != originPos)
        {
            transform.DORotate(new Vector3(0, -90), 2f).OnComplete(() =>
            {
                float duration = Vector2.Distance(transform.position, originPos);

                Debug.Log(Mathf.Round(duration / 10));

                transform.DOMove(originPos, Mathf.Round(duration / 10)).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    TakeOffBoat();
                    inventory.SetActive(true);

                });
            });
        }
    }
}
