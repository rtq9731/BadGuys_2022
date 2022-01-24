using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Tutorial
{
    public class FirstInteractTrigger : StoryTrigger
    {
        public override void OnTriggered()
        {
            DialogManager.Instance.SetDialaogs(datas.GetDialogs());
            Debug.Log("¤¾¤·");
        }
    }
}
