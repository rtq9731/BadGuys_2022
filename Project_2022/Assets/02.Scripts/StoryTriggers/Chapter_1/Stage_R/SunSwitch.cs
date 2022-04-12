using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Triggers.Switch.Chapter1.StageR
{
    public class SunSwitch : GetTubeSwitch
    {
        [SerializeField] Light sun = null;
        private void Start()
        {
            GetComponent<ColorRemoveObjParent>().onInteract += Fire;
            GetComponent<ColorRemoveObjParent>().onInteract += () => { GetComponent<ColorRemoveObjParent>().onInteract = null; };
            GetComponent<ColorRemoveObjParent>().onInteract += () => sun.gameObject.SetActive(false);
        }
    }
}
