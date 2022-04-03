using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GStageLightTrigger : MonoBehaviour
{
    [SerializeField] List<Light> lights = new List<Light>();

    private void Start()
    {
        lights.ForEach(item =>
        {
            item.gameObject.SetActive(false);
        });
    }

    public void SetActiveGroup(bool active)
    {
        lights.ForEach(item =>
        {
            item.gameObject.SetActive(active);
        });
    }
}
