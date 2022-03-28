using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1
{
    public class GetTubeSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<ColorRemoveObjParent>().onInteract += Fire;
            GetComponent<ColorRemoveObjParent>().onInteract += () => { GetComponent<ColorRemoveObjParent>().onInteract = null; };
        }
    }
}
