using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ButterflyTrigger : CameraBlending
{
    [SerializeField] ButterFlyScript butterfly = null;
    [SerializeField] GameObject butterfly_Idle = null;
    [SerializeField] Transform destination = null;
    [SerializeField] CinemachineVirtualCamera vcam = null;

    ButterflySkipBtn skipBtn = null;

    public event System.Action onComplete = null;

    public Collider[] cols;

    protected override void Start()
    {
        base.Start();
        skipBtn = FindObjectOfType<ButterflySkipBtn>(true);
    }

    private void Update()
    {
        if(isEndBlending)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) // 플레이어 충돌
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
                StartCoroutine(CameraBlendingCo());
                onComplete?.Invoke();
            });

            vcam.m_LookAt = butterfly.butterfly;
            vcam.gameObject.SetActive(true);
        }
    }

    void RemoveCol()
    {
        cols = gameObject.GetComponents<Collider>();

        for (int i = 0; i < gameObject.GetComponents<Collider>().Length; i++)
        {
            gameObject.GetComponents<Collider>()[i].enabled = false;
        }
    }
}
