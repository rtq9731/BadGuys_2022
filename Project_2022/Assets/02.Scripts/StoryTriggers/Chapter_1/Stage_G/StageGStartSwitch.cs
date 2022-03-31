using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageG
{
    public class StageGStartSwitch : TriggerSwitch
    {
        private void Start()
        {
            trigger.OnTriggered();
        }
    }
}

