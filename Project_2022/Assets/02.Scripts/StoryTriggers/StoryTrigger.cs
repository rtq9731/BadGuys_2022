using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public abstract class StoryTrigger : MonoBehaviour
    {
        [SerializeField] protected DialogDatas datas;
        public abstract void OnTriggered();
    }
}
