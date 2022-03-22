using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorRemoveObjParent : MonoBehaviour, IInteractAndGetItemObj, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
{
    [SerializeField] protected ItemInfo keyItem = null;
    [SerializeField] protected ItemInfo returnItem = null;

    [SerializeField] protected Outline outline = null;
    [SerializeField] protected GameObject itemObj = null;
    [SerializeField] protected GameObject dissolveObj = null;

    [SerializeField] protected float noiseScale = 50f;
    [SerializeField] protected float dissolveDuration = 2f;
    [SerializeField] protected float dissolveStrength = 50f;

    public Action onInteract = null;

    protected bool canInteract = true;

    protected Inventory inventory = null;

    protected Material dissolveMat = null;
    public virtual void Start()
    {
        onInteract += () => enabled = false;
        dissolveMat = dissolveObj.GetComponent<MeshRenderer>().material;
        dissolveMat.SetFloat("_NoiseScale", noiseScale);
        inventory = FindObjectOfType<Inventory>();
        outline.enabled = false;
    }

    public abstract void Interact(ItemInfo itemInfo, GameObject taker);

    public virtual void OnPlayerMouseEnter()
    {
        if (inventory.MainItem == keyItem && canInteract)
        {
            outline.enabled = true;
        }
    }

    public virtual void OnPlayerMouseExit()
    {
        outline.enabled = false;
    }
}
