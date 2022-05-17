using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2LastCutSceneTrigger : CollisionTimelineTrigger
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnComplete()
    {
        base.OnComplete();
        LoadingManager.LoadScene("Title", true);
    }
}
