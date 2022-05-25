using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTECollisionTrigger_3 : QTE_CollisionTrigger
{
    protected override void OnTriggered()
    {
        qm.GenerateQTEEvent(QTEPressType.Single, KeyCode.A, OnSuccessQTE, OnFailedQTE);
    }

    private void OnSuccessQTE()
    {
        selector.PlayTimeline(0);
        ChaseHPSystem.PlusHP(heal);
    }

    private void OnFailedQTE()
    {
        selector.PlayTimeline(1, () =>
        {
            qm.GenerateQTEEvent(QTEPressType.Single, KeyCode.D, OnFailedSuccessQTE, OnFailedFailedQTE);
        });
    }
    private void OnFailedSuccessQTE()
    {
        selector.PlayTimeline(2);
        ChaseHPSystem.PlusHP(heal / 2);
    }
    private void OnFailedFailedQTE()
    {
        selector.PlayTimeline(3);
    }
}
