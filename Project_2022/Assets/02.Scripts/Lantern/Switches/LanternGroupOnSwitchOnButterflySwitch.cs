using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternGroupOnSwitchOnButterflySwitch : LanternGroupOnSwitch
{
    [SerializeField] ButterflyTrigger butterflyTrigger = null;

    private void Start()
    {
        butterflyTrigger.onComplete += TurnOn;
        butterflyTrigger.onComplete += () => butterflyTrigger.onComplete -= TurnOn;
    }
}
