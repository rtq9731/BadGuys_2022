using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceLight : MonoBehaviour
{
    [SerializeField]
    private GameObject targetLight;
    [SerializeField]
    private float waitTime;
    private bool inPlayer;

    private void Awake()
    {
        if (targetLight == null)
            Debug.LogWarning("타겟 라이트가 안들어가있음");
        else
            targetLight.SetActive(false);
    }

    private void LightOn()
    {
        inPlayer = true;
        targetLight.SetActive(true);
    }

    private void LightOff()
    {
        inPlayer = false;
        StartCoroutine(LightCheck());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            LightOn();
    }

    private void OnTriggerExit(Collider other)
    {
        LightOff();
    }
    

    IEnumerator LightCheck()
    {
        yield return new WaitForSeconds(waitTime);

        if (inPlayer == false)
        {
            targetLight.SetActive(false);
        }
    }
}
