using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

namespace Triggers.Switch.Chapter1
{
    public class PictureInteractSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<ColorFillObj>()._onPlayerMouseEnter += Fire;
        }
    }
}
