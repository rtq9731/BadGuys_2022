using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmeraldCaseOpen : MonoBehaviour, IInteractableItem
{
    public GameObject emeraldCase;
    public GameObject playerCam;
    public GameObject emeraldCam;

    public PlayerController playerController;

    float originPosY;
    private void Start()
    {
        originPosY = transform.position.y;
    }

    IEnumerator ClearStageG()
    {
        emeraldCam.SetActive(true);
        playerController.enabled = false;
        while (Vector3.Distance(emeraldCam.transform.position, playerCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        emeraldCase.transform.DOLocalMove(new Vector3(0, 0, -3f), 1f).OnComplete(() =>
        {
            LoadingTrigger.Instance.Ontrigger(3f);
        });
    }

    public void Interact(GameObject taker)
    {
        Debug.Log("asd");
        transform.DOLocalMoveY(-0.58f, 0.2f).OnComplete(() =>
        {
            transform.DOMoveY(originPosY, 0.2f);
            StartCoroutine(ClearStageG());
        });
    }
}
