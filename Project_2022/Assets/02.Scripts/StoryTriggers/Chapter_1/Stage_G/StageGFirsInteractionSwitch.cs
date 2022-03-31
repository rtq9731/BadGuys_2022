using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageG
{
    public class StageGFirsInteractionSwitch : TriggerSwitch
    {
        [SerializeField] OutlinerOnMouseEnter[] pictures = null;

        private void Start()
        {
            foreach (var item in pictures)
            {
                item._onMouseEnter += Fire;
                item._onMouseEnter += () =>
                {
                    foreach (var item in pictures)
                    {
                        item._onMouseEnter -= Fire;
                    }
                };
            }
        }

    }
}