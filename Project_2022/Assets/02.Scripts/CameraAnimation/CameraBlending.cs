using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlending : MonoBehaviour
{
    public GameObject mainCam = null;
    public GameObject blendingCam = null;

    public PlayerController player;

    public bool isEndBlending = false;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    protected IEnumerator CameraBlendingCo()
    {
        player.enabled = false;
        isEndBlending = false;

        Vector3 camPos = new Vector3(mainCam.transform.position.x, 0, mainCam.transform.position.z);
        Vector3 playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);


        while(Vector3.Distance(camPos, playerPos) >= 0.1f)
        {
            camPos = new Vector3(mainCam.transform.position.x, 0, mainCam.transform.position.z);
            playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            yield return null;
        }

        player.enabled = true;
        isEndBlending = true;
    }

    public void StartCo()
    {
        StartCoroutine(CameraBlendingCo());
    }
}
