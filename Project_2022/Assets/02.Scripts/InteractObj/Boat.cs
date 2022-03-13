using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boat : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject sunObj;
    [SerializeField]
    private GameObject boatCam;
    [SerializeField]
    private GameObject mainCam;


    private RectTransform rectTrm;

    private Vector3 originPos;

    private bool isMove = false;

    private void Start()
    {
        rectTrm = inventory.transform.GetChild(0).GetComponent<RectTransform>();
        originPos = transform.position;
    }

    private void Update()
    {
        ReturnMove();
    }

    public void Interact(GameObject taker)
    {
        if(transform.position == originPos)
        {
            StartCoroutine(StartMove());
        }
    }

    // 플레이어 이동제한, UI없애는 함수
    void SetPlayerInput(bool isCanMove)
    {
        isMove = isCanMove;
    }

    // 보트 출발하는 함수
    IEnumerator StartMove()
    {
        SetPlayerInput(true);
        boatCam.SetActive(true);
        inventory.SetActive(false);

        while(Vector3.Distance(boatCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.4f);
        transform.DOMoveX(230f, 20f).OnComplete(() =>
        {
            inventory.SetActive(true);
            SetPlayerInput(false);
        });
    }

    // 처음 자리로 돌아가는 함수
    private void ReturnMove()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            DOTween.KillAll();

            TurnBoat();
            inventory.SetActive(false);
            transform.DOMove(originPos, 5f).OnComplete(() =>
            {
                TakeOffBoat();
                inventory.SetActive(true);
            });
        }
    }

    // 보트에서 내리는 함수
    private void TakeOffBoat()
    {
        boatCam.SetActive(false);
    }

    // 보트 돌리는 함수
    private void TurnBoat()
    {
        if(transform.position == originPos)
        {
            transform.DORotate(new Vector3(0, 90), 2f);
        }
        else
        {
            transform.DORotate(new Vector3(0, -90), 2f);
        }
    }
}
