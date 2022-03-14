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
    [SerializeField]
    private GameObject playerCam;


    private RectTransform rectTrm;

    private Vector3 originPos;

    private bool isMove = false;

    Animator[] anim;

    private void Start()
    {
        rectTrm = inventory.transform.GetChild(0).GetComponent<RectTransform>();
        originPos = transform.position;

        anim = GetComponentsInChildren<Animator>();
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

    // �÷��̾� �̵�����, UI���ִ� �Լ�
    void SetPlayerInput(bool isCanMove)
    {
        isMove = isCanMove;
    }

    // ��Ʈ ����ϴ� �Լ�
    IEnumerator StartMove()
    {
        SetPlayerInput(true);
        boatCam.SetActive(true);
        inventory.SetActive(false);
        
        for(int i = 0; i < 2; i ++)
        {
            anim[i].SetBool("IsBoat", true);
        }

        playerCam.GetComponentInParent<PlayerController>().camTrm = boatCam.transform;

        while (Vector3.Distance(boatCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.4f);
        transform.DOMoveX(230f, 15f).SetEase(Ease.Unset).OnComplete(() =>
        {
            inventory.SetActive(true);
            SetPlayerInput(false);
        });
    }

    // ó�� �ڸ��� ���ư��� �Լ�
    private void ReturnMove()
    {
        if(Input.GetKeyDown(KeyCode.R) && isMove)
        {
            DOTween.KillAll();

            TurnBoat();
            inventory.SetActive(false);
            
        }
    }

    // ��Ʈ���� ������ �Լ�
    private void TakeOffBoat()
    {
        transform.DORotate(new Vector3(0, 90), 2f).OnComplete(() =>
        {
            playerCam.GetComponentInParent<PlayerController>().camTrm = playerCam.transform;
            for (int i = 0; i < 2; i++)
            {
                anim[i].SetBool("IsBoat", false);
            }
            boatCam.SetActive(false);
        });
    }


    // ��Ʈ ������ �Լ�
    private void TurnBoat()
    {
        if(transform.position != originPos)
        {
            transform.DORotate(new Vector3(0, -90), 2f).OnComplete(() =>
            {
                transform.DOMove(originPos, 5f).OnComplete(() =>
                {
                    TakeOffBoat();
                    inventory.SetActive(true);

                });
            });
        }
    }
}
