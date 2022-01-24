using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers
{
    public abstract class StoryTrigger : MonoBehaviour
    {
        public abstract void OnTriggered();
    }
}
