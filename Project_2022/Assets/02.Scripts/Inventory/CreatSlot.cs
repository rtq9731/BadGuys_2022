using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatSlot : MonoBehaviour
{
    [SerializeField]
    public GameObject slot;

    public void CreatingSlot()
    {
        Instantiate(slot, this.gameObject.transform);
    }
}
