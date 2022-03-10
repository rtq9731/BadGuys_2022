using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_InkTube : Item
{
    [SerializeField] Color color;
    [SerializeField] GameObject tubeBody = null;

    private void Start()
    {
        tubeBody.GetComponent<MeshRenderer>().material.color = color;
    }

    public override void Interact(GameObject taker)
    {
        base.Interact(taker);
    }
}
