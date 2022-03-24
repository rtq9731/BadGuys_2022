using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour, IInteractableItem, IPlayerMouseEnterHandler, IPlayerMouseExitHandler, IGetPlayerMouseHandler
{
    private Outline outline = null;

    private Animator anim = null;

    [SerializeField] string triggerName = "";

    bool isActive = true;

    public event System.Action onInteract = null;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        outline = GetComponentInChildren<Outline>();
        outline.enabled = false;
    }

    public void OnPlayerMouseEnter()
    {
        if (!isActive)
            return;

        outline.enabled = true;
    }

    public void OnGetPlayerMouse()
    {
        if (!isActive)
            return;

        outline.enabled = true;
    }

    public void OnPlayerMouseExit()
    {
        if (!isActive)
            return;

        outline.enabled = false;
    }


    public void Interact(GameObject taker)
    {
        if (!isActive)
            return;

        anim.SetTrigger(triggerName);
        outline.enabled = false;
        onInteract?.Invoke();
    }

    public void SetActive(bool active)
    {
        enabled = active;
        isActive = active;
    }
}
