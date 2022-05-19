using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2FivethCutSceneTrigger : CollisionTimelineTrigger
{
    [SerializeField] GameObject kidnapperForCutScene = null;
    [SerializeField] GameObject qteCam = null;

    public override void OnStart()
    {
        base.OnStart();
        qteCam.SetActive(true);
    }

    public override void OnComplete()
    {
        base.OnComplete();
        qteCam.SetActive(false);
        ChaseKidnapperAI ai = FindObjectOfType<ChaseKidnapperAI>(true);
        kidnapperForCutScene.SetActive(false);
        ai.SetDestination(8, () =>
        {
            ai.SetDestination(9, () => 
            {
                
                ai.gameObject.SetActive(false);
            });
            
        });
    }
}
