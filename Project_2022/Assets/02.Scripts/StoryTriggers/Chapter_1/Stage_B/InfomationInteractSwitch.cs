using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageB
{
    public class InfomationInteractSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<StoneLightSign>()._onInteract += Fire;
            GetComponent<StoneLightSign>()._onInteract += () =>
            {
                GetComponent<StoneLightSign>()._onInteract -= Fire;
            };
        }
    }
}
