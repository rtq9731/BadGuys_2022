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
        ai.SetDestination(4, () =>
        {
            ai.SetPos(5);
            ai.gameObject.SetActive(false);
        });
    }
}
