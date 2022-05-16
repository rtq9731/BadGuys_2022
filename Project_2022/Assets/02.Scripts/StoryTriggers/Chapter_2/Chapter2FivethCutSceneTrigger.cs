using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2FivethCutSceneTrigger : CollisionTimelineTrigger
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnComplete()
    {
        base.OnComplete();
        ChaseKidnapperAI ai = FindObjectOfType<ChaseKidnapperAI>(true);
        ai.SetDestination(8, () =>
        {
            ai.SetDestination(9, () => 
            {
                ai.gameObject.SetActive(false);
            });
            
        });
    }
}
