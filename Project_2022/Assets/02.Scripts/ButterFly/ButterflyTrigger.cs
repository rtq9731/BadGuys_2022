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

    ButterflySkipBtn skipBtn = null;

    public event System.Action onComplete = null;

    private void Start()
    {
        skipBtn = FindObjectOfType<ButterflySkipBtn>(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) // �÷��̾� �浹
        {
            butterfly_Idle.gameObject.SetActive(false);
            skipBtn.SetActive(true);

            butterfly.transform.position = butterfly_Idle.transform.position;
            
            UIManager.Instance.OnCutSceneWithoutPause();
            
            butterfly.Disappear(destination, () =>
            {
                vcam.gameObject.SetActive(false);
                skipBtn.SetActive(false);
                UIManager.Instance.OnCutSceneOverWithoutClearDialog();
                gameObject.SetActive(false);
                onComplete?.Invoke();
            });
            vcam.m_LookAt = butterfly.butterfly;
            vcam.gameObject.SetActive(true);
        }
    }
}
