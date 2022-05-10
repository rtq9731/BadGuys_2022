using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2ScondCutSceneTrigger : CollisionTimelineTrigger
{
    [SerializeField] GameObject kidnapperForCutScene = null;
    [SerializeField] GameObject kidnapperForNextCutScene = null;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnComplete()
    {
        base.OnComplete();
        kidnapperForCutScene.SetActive(false);
        ChaseKidnapperAI ai = FindObjectOfType<ChaseKidnapperAI>(true);
        ai.SetDestination(2, () =>
        {
            ai.SetPos(3);
            kidnapperForNextCutScene.SetActive(true);
            ai.gameObject.SetActive(false);
        });
    }
}
