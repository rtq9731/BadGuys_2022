using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2ThirdCutSceneTrigger : CollisionTimelineTrigger
{
    [SerializeField] GameObject kidnapperForCutScene = null;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnComplete()
    {
        base.OnComplete();

        ChaseKidnapperAI ai = FindObjectOfType<ChaseKidnapperAI>(true);

        kidnapperForCutScene.SetActive(false);
        ai.gameObject.SetActive(true);
        ai.SetDestination(2, () =>
        {
            Debug.Log("세번째 목적지 도착!");
            ai.gameObject.SetActive(false);
        });
    }
}
