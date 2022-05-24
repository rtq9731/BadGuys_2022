using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTECollisionTrigger_1 : QTE_CollisionTrigger
{
    protected override void OnTriggered()
    {
        qm.GenerateQTEEvent(QTEPressType.Single, KeyCode.E, () => selector.PlayTimeline(0, true), () => selector.PlayTimeline(1, true));
    }
}
