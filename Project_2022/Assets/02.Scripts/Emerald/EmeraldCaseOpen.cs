using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Triggers;

public class EmeraldCaseOpen : MonoBehaviour, IInteractableItem
{
    public GameObject emeraldCase;
    public GameObject playerCam;
    public GameObject emeraldCam;

    public PlayerController playerController;

    [SerializeField]
    StoryTrigger stageGClearTrigger;
    [SerializeField]
    SoundScript sound;

    float originPosY;

    bool isClear;
    private void Start()
    {
        originPosY = transform.position.y;
        sound = GetComponent<SoundScript>();
    }

    IEnumerator ClearStageG()
    {
        emeraldCam.SetActive(true);
        playerController.enabled = false;
        UIManager.Instance.OnCutSceneWithoutPause();

        while (Vector3.Distance(emeraldCam.transform.position, playerCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        stageGClearTrigger.OnTriggered();

        emeraldCase.transform.DOLocalMove(new Vector3(0, 0, -3f), 1f).OnComplete(() =>
        {
            //UIManager._instance.OnCutSceneOverWithoutClearDialog();
            LoadingTrigger.Instance.Ontrigger(3f);
        });
    }

    public void Interact(GameObject taker)
    {
        if (!isClear)
        {
            Debug.Log("asd");
            isClear = true;
            sound.Play();
            transform.DOLocalMoveY(-0.58f, 0.2f).OnComplete(() =>
            {
                transform.DOMoveY(originPosY, 0.2f);
                StartCoroutine(ClearStageG());
            });
        }
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
