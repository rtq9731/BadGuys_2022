using UnityEngine;

namespace Triggers.Switch
{
    [RequireComponent(typeof(BoxCollider))]
    public class CollisionTriggerSwitch : TriggerSwitch
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Fire();
            }
        }

        public override void Fire()
        {
            trigger.OnTriggered();
            gameObject.SetActive(false);
        }
    }
}
