using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class CollisionTimelineTrigger : MonoBehaviour
{
    [SerializeField] GameObject timeline = null;

    virtual public void OnStart()
    {
        UIManager.Instance.OnCutScene();
    }

    virtual public void OnComplete()
    {
        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timeline.gameObject.SetActive(true);
        }
    }
}
