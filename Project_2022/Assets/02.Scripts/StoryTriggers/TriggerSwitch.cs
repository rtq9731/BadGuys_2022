using UnityEngine;

namespace Triggers.Switch.Tutorial
{
    public class TriggerSwitch : MonoBehaviour
    {
        [SerializeField] protected StoryTrigger trigger;
        public virtual void Fire()
        {
            enabled = false;
        }
    }
}
