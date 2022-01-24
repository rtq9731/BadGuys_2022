using UnityEngine;

namespace Triggers.Switch.Tutorial
{
    public class TriggerSwitch : MonoBehaviour
    {
        [SerializeField] protected Triggers.StoryTrigger storyTrigger;

        public virtual void Fire()
        {
            enabled = false;
        }
    }
}
