using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Triggers;

public class RotationPuzzle : MonoBehaviour
{
    [SerializeField] List<RotationPuzzleElement> elements = new List<RotationPuzzleElement>();
    [SerializeField] List<MonoBehaviour> myScripts = new List<MonoBehaviour>();
    [SerializeField] StoryTrigger completeTrigger = null;
    [SerializeField] GStageLightTrigger lightTrigger = null;
    [SerializeField] GameObject completeWall = null;

    [SerializeField] float errorRange = 10f;

    [SerializeField] GameObject vCamComplete = null;
    [SerializeField] SpriteRenderer completeSR = null;

    [SerializeField] GameObject keyPiece = null;
    private void Start()
    {
        foreach (var item in elements)
        {
            item._onRotationChanged += OnElementRotate;
        }
    }

    private void OnElementRotate()
    {
        List<RotationPuzzleElement> items = new List<RotationPuzzleElement>();

        Debug.Log((int)elements[0].GetPictureRotationZ() == (int)elements[2].GetPictureRotationZ());
        Debug.Log((int)elements[0].GetPictureRotationZ() == (int)elements[1].GetPictureRotationZ());
        if (!((int)elements[0].GetPictureRotationZ() == (int)elements[1].GetPictureRotationZ()
            && (int)elements[0].GetPictureRotationZ() == (int)elements[2].GetPictureRotationZ()))
            return;

        OnCompletePuzzle(elements[0].GetPictureRotationZ());
    }

    private void OnCompletePuzzle(float destRot)
    {
        vCamComplete.SetActive(true);
        completeSR.transform.rotation = Quaternion.Euler(new Vector3(0, 0, destRot));
        completeSR.gameObject.SetActive(true);
        completeSR.material.SetFloat("_DissolveAmount", 0f);
        UIManager._instance.OnCutScene();

        foreach (var item in elements)
        {
            item.DORotateToAnswer(destRot);
        }

        completeSR.material.DOFloat(1, "_DissolveAmount", 3f).OnComplete(() => 
        {
            completeSR.material.DOFloat(0, "_DissolveAmount", 3f).OnComplete(() =>
            {
                UIManager._instance.OnCutSceneOverWithoutClearDialog();
                completeSR.gameObject.SetActive(false);
                completeTrigger.OnTriggered();
                vCamComplete.SetActive(false);
                completeWall.SetActive(false);
                lightTrigger.SetActiveGroup(true);
                keyPiece.SetActive(true);
                Inventory.Instance.PickUpItem(keyPiece.GetComponent<Item>().itemInfo, keyPiece, keyPiece);
                enabled = false;
                myScripts.ForEach(item => item.enabled = false);
            });
        });

    }
}
