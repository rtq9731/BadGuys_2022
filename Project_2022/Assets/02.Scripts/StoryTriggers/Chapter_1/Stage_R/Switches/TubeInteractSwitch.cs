using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

namespace Triggers.Switch.Chapter1.StageR
{
    public class TubeInteractSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<Item>().onInteract += Fire;
            GetComponent<Item>().onInteract += () => { GetComponent<Item>().onInteract -= Fire; };
        }
    }
}
