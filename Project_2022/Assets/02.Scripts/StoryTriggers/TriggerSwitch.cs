using UnityEngine;

namespace Triggers.Switch
{
    public class TriggerSwitch : MonoBehaviour
    {
        [SerializeField] protected StoryTrigger trigger;
        public virtual void Fire()
        {
            trigger.OnTriggered();
            enabled = false;
        }
    }
}
