using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureDissolveObj : MonoBehaviour
{
    [SerializeField] Texture2D mainTex = null;

    private void Awake()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", mainTex);
    }
}
