using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1
{
    public class RStageClearTrigger : StoryTrigger
    {
        public override void OnTriggered()
        {
            DialogManager.Instance.SetDialaogs(datas.GetDialogs());
        }
    }
}