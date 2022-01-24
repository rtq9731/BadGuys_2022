using UnityEngine;

namespace Triggers.Switch.Tutorial
{
    public class TriggerSwitch : MonoBehaviour
    {
        [SerializeField] protected Triggers.StoryTrigger storyTrigger;
        public bool isTriggered = false;

        public virtual void Fire()
        {
            isTriggered = true;
            if (isTriggered)
                return;
        }
    }
}
