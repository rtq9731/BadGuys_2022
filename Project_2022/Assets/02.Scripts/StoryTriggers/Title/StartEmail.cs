using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Title
{
    public class StartEmail : EmailTrigger
    {
        private void Start()
        {
            OnTriggered();
        }
    }
}
