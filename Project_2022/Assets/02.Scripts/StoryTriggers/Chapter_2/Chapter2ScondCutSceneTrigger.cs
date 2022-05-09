using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2ScondCutSceneTrigger : CollisionTimelineTrigger
{
    [SerializeField] GameObject kidnapperForCutScene = null;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnComplete()
    {
        base.OnComplete();
        kidnapperForCutScene.SetActive(false);
        ChaseKidnapperAI ai = FindObjectOfType<ChaseKidnapperAI>(true);
        ai.SetDestination(1, () =>
        {
            ai.SetDestination(2, () =>
            {
                ai.gameObject.SetActive(false);
            });
        });
    }
}
