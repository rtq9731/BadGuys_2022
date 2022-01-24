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
            if (!isTriggered)
            {
                foreach (var item in FindObjectsOfType<Item>())
                {
                    item.onInteract -= Fire;
                }
                isTriggered = true;

                storyTrigger.OnTriggered();
            }
            else
            {
                return;
            }
        }
    }


}
