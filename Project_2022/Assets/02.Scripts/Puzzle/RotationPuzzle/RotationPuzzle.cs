using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationPuzzle : MonoBehaviour
{
    [SerializeField] List<RotationPuzzleElement> elements = new List<RotationPuzzleElement>();

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
        var items = from item in elements
                    select -errorRange <= item.GetPictureRotationZ() && errorRange >= item.GetPictureRotationZ();

        if(items.Count() == elements.Count)
        {
            // OnCompletePuzzle();
        }
    }

    private void OnCompletePuzzle()
    {
        vCamComplete.SetActive(true);
        FindObjectOfType<UIManager>().OnCutScene();

        foreach (var item in elements)
        {
            item.DORotateToAnswer();
        }

        completeSR.material.DOFloat(1, "_DissolveAmount", 3f).OnComplete(() => 
        {
            completeSR.material.DOFloat(0, "_DissolveAmount", 3f).OnComplete(() =>
            {
                FindObjectOfType<UIManager>().OnCutSceneOver();
                completeSR.gameObject.SetActive(false);
                vCamComplete.SetActive(false);
                enabled = false;
            });
        });

    }
}
