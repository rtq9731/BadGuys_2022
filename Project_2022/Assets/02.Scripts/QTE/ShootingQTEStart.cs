using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingQTEStart : MonoBehaviour
{
    [SerializeField] PlayerController playerController = null;
    [SerializeField] QTEManager manager = null;
    [SerializeField] GameObject QTEUi = null;
    [SerializeField] GameObject QTECamera = null;
    [SerializeField] GameObject mainCamera = null;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(CameraMove());
            UIManager.Instance.OnCutScene();
            playerController.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            
        }
    }

    IEnumerator CameraMove()
    {
        QTECamera.SetActive(true);
        while (Vector3.Distance(QTECamera.transform.position, mainCamera.transform.position) >= 0.1f)
        {
            yield return null;
        }

        QTEUi.SetActive(true);
        manager.GenerateQTEEvent();
    }
}
