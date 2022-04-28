using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

[RequireComponent(typeof(SignalReceiver))]
public class CollisionTimelineTrigger : MonoBehaviour
{
    [SerializeField] GameObject timeline = null;
    [SerializeField] GameObject vCamCutScene = null;
    SignalReceiver receiver = null;

    UnityEvent _onTimelineStart = new UnityEvent();
    UnityEvent _onTimelineEnd = new UnityEvent();

    private void Awake()
    {
        receiver = GetComponent<SignalReceiver>();
    }

    virtual public void OnStart()
    {
        Debug.Log("Ÿ�Ӷ��� ����!");
        UIManager._instance.OnCutScene();
        Debug.Log(UIManager._instance.isOnCutScene);
        vCamCutScene.SetActive(true);
    }

    virtual public void OnComplete()
    {
        Debug.Log("Ÿ�Ӷ��� ��!");
        vCamCutScene.SetActive(false);
        UIManager._instance.OnCutSceneOverWithoutClearDialog();
        Debug.Log(UIManager._instance.isOnCutScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timeline.gameObject.SetActive(true);
        }
    }
}
