using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    Material lightMat = null;
    Light myLight = null;

    Color originColor = Color.white;

    private void Awake()
    {
        lightMat = GetComponent<MeshRenderer>().materials[1];
        myLight = GetComponentInChildren<Light>();
        lightMat.EnableKeyword("_EMISSION");
        originColor = lightMat.GetColor("_EmissionColor");
    }

    public void SetActiveLight(bool active)
    {
        lightMat.SetColor("_EmissionColor", active ? originColor : Color.black);
        myLight.gameObject.SetActive(active);
    }
}
