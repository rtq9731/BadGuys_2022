using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleInfo : MonoBehaviour, IInteractableItem
{
    [SerializeField] GameObject vCamInfo = null;

    bool isInteracting = false;

    public void Interact(GameObject taker)
    {
        if (isInteracting)
            return;

        StartCoroutine(ShowPicture());
    }

    IEnumerator ShowPicture()
    {
        vCamInfo.gameObject.SetActive(true);
        UIManager._instance.OnCutScene();
        yield return new WaitForSeconds(2f);
        vCamInfo.gameObject.SetActive(false);
        UIManager._instance.OnCutSceneOverWithoutClearDialog();
        isInteracting = false;
    }
}
