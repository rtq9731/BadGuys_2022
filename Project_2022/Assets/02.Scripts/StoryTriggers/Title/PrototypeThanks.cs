using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Title
{
    public class PrototypeThanks : EmailTrigger
    {
        private void OnDestroy()
        {
            OnTriggered();
        }
    }
}
