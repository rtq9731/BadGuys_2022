using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

[RequireComponent(typeof(SignalReceiver))]
public class CollisionTimelineTrigger : MonoBehaviour
{
    [SerializeField] GameObject timeLineObj = null;
    SignalReceiver receiver = null;

    [SerializeField] SignalAsset startSignal = null;
    [SerializeField] SignalAsset endSignal = null;

    UnityEvent _onTimelineStart = new UnityEvent();
    UnityEvent _onTimelineEnd = new UnityEvent();

    private void Awake()
    {
        receiver = GetComponent<SignalReceiver>();
    }

    private void Start()
    {
        _onTimelineStart.AddListener(OnStart);
        _onTimelineEnd.AddListener(OnComplete);

        receiver.AddReaction(startSignal, _onTimelineStart);
        receiver.AddReaction(endSignal, _onTimelineEnd);
    }

    virtual public void OnStart()
    {
        Debug.Log("타임라인 시작!");
        UIManager._instance.OnCutScene();
    }

    virtual public void OnComplete()
    {
        Debug.Log("타임라인 끝!");
        UIManager._instance.OnCutSceneOverWithoutClearDialog();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            timeLineObj.gameObject.SetActive(true);
        }
    }
}
