using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2FourthCutSceneTrigger : CollisionTimelineTrigger
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnComplete()
    {
        base.OnComplete();

        ChaseKidnapperAI ai = FindObjectOfType<ChaseKidnapperAI>(true);

        ai.gameObject.SetActive(true);
        ai.SetDestination(6, () =>
        {
            ai.gameObject.SetActive(false);
        });
    }
}
