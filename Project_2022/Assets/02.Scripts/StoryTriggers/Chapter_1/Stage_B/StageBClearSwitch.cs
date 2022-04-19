using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageB
{
    public class StageBClearSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<ColorFillObj_prcture>()._onComplete += Fire;

            GetComponent<ColorFillObj_prcture>()._onComplete += () =>
            {
                GetComponent<ColorFillObj_prcture>()._onComplete -= Fire;
                LoadingTrigger.Instance.Ontrigger(10f);
            };
        }
    }
}

