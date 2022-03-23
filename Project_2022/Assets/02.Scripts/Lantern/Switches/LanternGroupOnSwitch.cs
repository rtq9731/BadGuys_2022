using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LanternGroupOnSwitch : MonoBehaviour
{
    [SerializeField] protected LanternGroup lanternGroup = null;
    public void TurnOn()
    {
        lanternGroup.SetActive(true);
    }
}
