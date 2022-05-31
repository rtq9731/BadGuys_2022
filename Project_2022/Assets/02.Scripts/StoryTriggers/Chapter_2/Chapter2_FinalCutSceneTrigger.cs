using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class Chapter2_FinalCutSceneTrigger : MonoBehaviour
{
    public GameObject startTimeline = null;
    public GameObject aimTimeline = null;
    public GameObject finishTimeline = null;
    public GameObject cam = null;
    public Image black;

    private void Awake()
    {
        startTimeline.SetActive(false);
        aimTimeline.SetActive(false);
        finishTimeline.SetActive(false);
        cam.SetActive(false);
    }

    virtual public void OnStart()
    {
        UIManager.Instance.OnCutScene();
    }

    virtual public void OnComplete()
    {
        finishTimeline.SetActive(false);
        black.color = new Color(0, 0, 0, 1);
        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
        LoadingManager.LoadScene("Title", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            startTimeline.gameObject.SetActive(true);
            transform.GetComponent<Collider>().enabled = false;
        }   
    }

    public void AimCutScene()
    {
        aimTimeline.SetActive(true);
        startTimeline.SetActive(false);
    }

    public void FinishCutScene()
    {
        cam.SetActive(false);
        finishTimeline.SetActive(true);
        aimTimeline.SetActive(false);
    }
}
