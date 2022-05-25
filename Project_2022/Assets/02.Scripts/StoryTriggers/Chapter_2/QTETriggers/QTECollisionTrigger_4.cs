using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTECollisionTrigger_4 : QTE_CollisionTrigger
{
    protected override void OnTriggered()
    {
        qm.GenerateQTEEvent(QTEPressType.Single, KeyCode.W, OnSuccessQTE, OnFailedQTE);
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
            qm.GenerateQTEEvent(QTEPressType.Roll, KeyCode.E, OnSuccesSuccessQTE, OnSuccesFailedQTE);
        });
    }

    private void OnSuccesSuccessQTE()
    {
        selector.PlayTimeline(2);
        ChaseHPSystem.PlusHP(heal / 3);
    }

    private void OnSuccesFailedQTE()
    {
        selector.PlayTimeline(3);
    }
}
