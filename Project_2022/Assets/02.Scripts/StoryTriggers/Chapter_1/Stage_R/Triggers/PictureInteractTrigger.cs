using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

namespace Triggers.Switch.Chapter1
{
    public class PictureInteractTrigger : StoryTrigger
    {
        public override void OnTriggered()
        {
            DialogManager.Instance.SetDialaogs(datas.GetDialogs());
        }
    }
}
