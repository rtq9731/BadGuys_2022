using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class Chapter2_FinalCutSceneTrigger : MonoBehaviour
{
    public GameObject timeline = null;
    public Action _onStart;
    public Action _onComplete;

    virtual public void OnStart()
    {
        UIManager.Instance.OnCutScene();
        _onStart?.Invoke();
    }

    virtual public void OnComplete()
    {
        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
        timeline.gameObject.SetActive(false);
        _onComplete?.Invoke();
        LoadingManager.LoadScene("Title", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timeline.gameObject.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
