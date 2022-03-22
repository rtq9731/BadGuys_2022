using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Tutorial
{
    public class FirstInteractionSwitch : TriggerSwitch
    {
        private void Start()
        {
            foreach (var item in FindObjectsOfType<Item>())
            {
                item.onInteract += Fire;
            }
        }

        public override void Fire()
        {
            foreach (var item in FindObjectsOfType<Item>())
            {
                item.onInteract -= Fire;
            }
            trigger.OnTriggered();
            enabled = false;
        }
    }


}
