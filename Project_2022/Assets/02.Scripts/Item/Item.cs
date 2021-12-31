using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    public ItemInfo itemInfo;

    private void Update()
    {
        PickUp();
    }

    public void PickUp()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
}
