using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2FirstCutSceneTrigger : CollisionTimelineTrigger
{
    [SerializeField] GameObject kidnapper = null;
    [SerializeField] GameObject aiObj = null;
    [SerializeField] GameObject blackCar = null;

    public override void OnStart()
    {
        base.OnStart();
    }
    
    public override void OnComplete()
    {
        base.OnComplete();
        kidnapper.SetActive(false);
        blackCar.SetActive(false);
        aiObj.SetActive(true);
    }
}
