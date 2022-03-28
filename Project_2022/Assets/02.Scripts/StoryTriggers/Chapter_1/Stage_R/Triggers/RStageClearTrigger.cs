using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Chapter1.StageR
{
    public class RStageClearTrigger : StoryTrigger
    {
        public override void OnTriggered()
        {
            DialogManager.Instance.SetDialaogs(datas.GetDialogs());
        }
    }
}