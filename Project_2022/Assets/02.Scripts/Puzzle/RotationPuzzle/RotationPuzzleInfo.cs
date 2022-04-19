using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleInfo : MonoBehaviour, IInteractableItem
{
    [SerializeField] RotationPuzzle puzzle = null;

    bool isInteracting = false;

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }

    public void Interact(GameObject taker)
    {
        if (isInteracting)
            return;

        isInteracting = true; 
        puzzle.SetPuzzle();
        GetComponent<OutlinerOnMouseEnter>().enabled = false;
    }
}
