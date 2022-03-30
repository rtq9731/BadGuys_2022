using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageB
{
    public class FirstZoneInteractSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<ButterflyTrigger>().onComplete += Fire;
        }
    }
}
