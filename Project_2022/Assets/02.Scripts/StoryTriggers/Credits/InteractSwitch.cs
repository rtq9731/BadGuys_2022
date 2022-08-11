using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;


namespace Triggers.Switch.Tutorial
{
    public class InteractSwitch : TriggerSwitch
    {
        void Start()
        {
            GetComponent<InteractItems>().OnPlayerMouseEnter += Fire;
            GetComponent<InteractItems>().OnPlayerMouseEnter += () => { GetComponent<InteractItems>().OnPlayerMouseEnter -= Fire; };
        }

    }
}

