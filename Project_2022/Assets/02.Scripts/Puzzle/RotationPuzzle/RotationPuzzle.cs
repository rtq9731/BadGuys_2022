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
        items = elements.FindAll(item => Mathf.Abs(item.GetPictureRotationZ() % 360) <= errorRange);

        if (items.ToList().Count == elements.Count)
        {
            OnCompletePuzzle();
        }
    }

    private void OnCompletePuzzle()
    {
        vCamComplete.SetActive(true);
        completeSR.gameObject.SetActive(true);
        completeSR.material.SetFloat("_DissolveAmount", 0f);
        UIManager._instance.OnCutScene();

        foreach (var item in elements)
        {
            item.DORotateToAnswer();
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
                enabled = false;
                myScripts.ForEach(item => item.enabled = false);
            });
        });

    }
}
