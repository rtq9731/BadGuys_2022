using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

namespace Triggers.Chapter1.StageR
{
    public class TubeInteractTrigger : StoryTrigger
    {
        public override void OnTriggered()
        {
            DialogManager.Instance.SetDialaogs(datas.GetDialogs());
        }
    }
}
