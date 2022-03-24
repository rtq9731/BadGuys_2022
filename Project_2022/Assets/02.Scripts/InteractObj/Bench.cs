using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bench : MonoBehaviour, IInteractableItem
{
    private Outline outline = null;
    private OutlinerOnMouseEnter outliner = null;

    private Animator anim = null;

    [SerializeField] string triggerName = "";

    public event System.Action onInteract = null;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        outline = GetComponentInChildren<Outline>();
        outliner = GetComponentInChildren<OutlinerOnMouseEnter>();
    }

    public void Interact(GameObject taker)
    {
        anim.SetTrigger(triggerName);
        onInteract?.Invoke();
    }

    public void SetActive(bool active)
    {
        outline.enabled = active;
        outliner.enabled = active;
    }
}
