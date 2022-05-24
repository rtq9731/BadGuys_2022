using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTECollisionTrigger_1 : QTE_CollisionTrigger
{
    protected override void OnTriggered()
    {
        qm.GenerateQTEEvent(QTEPressType.Single, KeyCode.E, OnSuccessQTE, OnFailedQTE);
    }

    private void OnSuccessQTE()
    {
        selector.PlayTimeline(0);
        ChaseHPSystem.PlusHP(heal);
    }

    private void OnFailedQTE()
    {
        selector.PlayTimeline(1);
    }
}
