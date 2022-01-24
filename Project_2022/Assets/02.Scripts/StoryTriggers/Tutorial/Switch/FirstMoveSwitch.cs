using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Tutorial
{
    public class FirstMoveSwitch : TriggerSwitch
    {
        private void Start()
        {

        }

        public override void Fire()
        {
            storyTrigger.OnTriggered();
        }
    }


}
