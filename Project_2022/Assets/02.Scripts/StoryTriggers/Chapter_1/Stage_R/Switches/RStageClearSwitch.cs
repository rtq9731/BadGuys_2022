using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageR
{
    public class RStageClearSwitch : TriggerSwitch
    {
        private void Start()
        {
            GetComponent<ColorFillObj>()._onComplete += Fire;
            GetComponent<ColorFillObj>()._onComplete += () => { GetComponent<ColorFillObj>()._onComplete -= Fire; };
            LoadingTrigger.Instance.Ontrigger();
        }
    }
}