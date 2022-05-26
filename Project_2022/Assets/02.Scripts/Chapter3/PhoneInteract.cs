using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteract : MonoBehaviour, IInteractableItem
{
    public ItemInfo itemInfo;
    
    public CreateRenderTextureCam createRender;
    public InventoryItemPanel itemPanel;
    public GameObject iconCamObj;
    public GameObject phonePre;
    public float size;

    public void Interact(GameObject taker)
    {
    
    }
    
    public bool CanInteract()
    {
        return true;
    }
}
