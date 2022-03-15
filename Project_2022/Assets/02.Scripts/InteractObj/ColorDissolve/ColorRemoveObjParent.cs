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

    [SerializeField] protected float dissolveDuration = 2f;
    [SerializeField] protected float dissolveStrength = 50f;

    Inventory inventory = null;

    protected Material dissolveMat = null;
    private void Start()
    {
        dissolveMat = dissolveObj.GetComponent<MeshRenderer>().material;
        inventory = FindObjectOfType<Inventory>();
        outline.enabled = false;
    }

    public abstract void Interact(ItemInfo itemInfo, GameObject taker);

    public virtual void OnPlayerMouseEnter()
    {
        if ( inventory.MainItem == keyItem)
        {
            outline.enabled = true;
        }
    }

    public virtual void OnPlayerMouseExit()
    {
        outline.enabled = false;
    }
}
