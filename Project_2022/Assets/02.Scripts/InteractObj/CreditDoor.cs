using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CreditDoor : MonoBehaviour, IInteractableItem
{
    [SerializeField] CreditMove creditMove = null;
    [SerializeField] PlayableDirector pd = null;

    bool isOver = false;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact(GameObject taker)
    {
        UIManager.Instance.OnCutScene();
        pd.Play();
    }
}
