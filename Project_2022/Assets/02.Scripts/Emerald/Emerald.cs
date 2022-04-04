using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Emerald : MonoBehaviour ,IInteractableItem
{

    public GameObject emeraldCase;
    public GameObject playerCam;
    public GameObject emeraldCam;

    public PlayerController playerController;

    public BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    public void Interact(GameObject taker)
    {
        StartCoroutine(ClearStageG());
    }

    IEnumerator ClearStageG()
    {
        emeraldCam.SetActive(true);
        playerController.enabled = false;
        boxCollider.enabled = false;
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
}
