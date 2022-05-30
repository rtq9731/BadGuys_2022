using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookNumber : MonoBehaviour
{
    public GameObject lookCamera;
    public GameObject qteObj;

    private void Awake()
    {
        lookCamera.SetActive(false);
        qteObj.SetActive(false);
    }

    public void LookOnStart()
    {
        lookCamera.SetActive(true);
        qteObj.SetActive(true);
    }

    public void TextOn()
    {
        Debug.Log("Tlqkf Tlqkf Tlqkf");
    }
}
