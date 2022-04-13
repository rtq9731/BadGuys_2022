using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

namespace Triggers.Switch.Chapter1.StageR
{
    public class TubeInteractSwitch : TriggerSwitch
    {
        [SerializeField] MeshCollider mesh;
        private void Start()
        {
            GetComponent<Item>().onInteract += Fire;
            GetComponent<Item>().onInteract += () => { mesh.enabled = true; };
            GetComponent<Item>().onInteract += () => { GetComponent<Item>().onInteract -= Fire; };
            GetComponent<Item>().onInteract += () =>
            {
                foreach (var item in FindObjectsOfType<TubeInteractSwitch>())
                {
                    item.RemoveActions();
                }
            };
        }

        private void RemoveActions()
        {
            GetComponent<Item>().onInteract -= Fire;
        }

    }
}
