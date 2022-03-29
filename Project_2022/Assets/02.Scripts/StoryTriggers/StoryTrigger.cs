using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public class StoryTrigger : MonoBehaviour
    {
        [SerializeField] protected DialogDatas datas;
        public virtual void OnTriggered()
        {
            DialogManager.Instance.SetDialaogs(datas.GetDialogs());
        }
    }
}
