using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageB
{
    public class PictureInteractSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<ColorFillObj_prcture>()._onPlayerMouseEnter += Fire;

            GetComponent<ColorFillObj_prcture>()._onPlayerMouseEnter += () =>
            {
                GetComponent<ColorFillObj_prcture>()._onPlayerMouseEnter -= Fire;
            };
        }
    }
}
