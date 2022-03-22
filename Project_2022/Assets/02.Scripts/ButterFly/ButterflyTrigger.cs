using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ButterflyTrigger : MonoBehaviour
{
    [SerializeField] ButterFlyScript butterfly = null;
    [SerializeField] GameObject butterfly_Idle = null;
    [SerializeField] Transform destination = null;
    [SerializeField] CinemachineVirtualCamera vcam = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) // 플레이어 충돌
        {
            butterfly_Idle.gameObject.SetActive(false);
            butterfly.transform.position = transform.position;
            UIManager._instance.OnCutScene();
            butterfly.Disappear(destination, () =>
            {
                vcam.gameObject.SetActive(false);
                UIManager._instance.OnCutSceneOver();
                gameObject.SetActive(false);
            });
            vcam.m_LookAt = butterfly.transform;
            vcam.gameObject.SetActive(true);
        }
    }
}
