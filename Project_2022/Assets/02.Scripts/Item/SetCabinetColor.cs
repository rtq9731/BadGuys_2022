using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCabinetColor : MonoBehaviour
{
    public Material[] materials;

    public Color color;
    void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            materials[i] = transform.GetChild(i).GetComponent<MeshRenderer>().material;
            materials[i].color = color;
            Debug.Log(i);
        }
    }
}
