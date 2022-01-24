using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Tutorial
{
    public class GameStartTrigger : StoryTrigger
    {
        [SerializeField] DialogDatas datas;

        public override void OnTriggered()
        {
            DialogManager.Instance.SetDialaogs(datas.GetDialogs());
        }

        private void Start()
        {
            OnTriggered();
        }
    }
}
