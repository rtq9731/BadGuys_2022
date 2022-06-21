using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationButterfly : MonoBehaviour
{
    [SerializeField] int curidx = 0;
    [SerializeField] DestinationButterflyTrigger[] triggers;

    private void Start()
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].onTrigger += OnButterflyTriggered;
        }
        triggers[curidx].gameObject.SetActive(true);
    }

    public void OnButterflyTriggered()
    {
        curidx++;
        triggers[curidx].gameObject.SetActive(true);
    }
}
