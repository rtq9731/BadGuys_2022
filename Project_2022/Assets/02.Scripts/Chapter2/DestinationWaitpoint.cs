using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationWaitpoint : MonoBehaviour
{
    [SerializeField] PlayerCollisionTrigger trigger = null;

    public void SetWait(RunnerAI runner, System.Action callBack)
    {
        runner.gameObject.SetActive(false);

        if (trigger.isTriggered)
        {
            runner.gameObject.SetActive(true);
            callBack();
            trigger.onTrigger = null;
            return;
        }

        trigger.onTrigger += () =>
        {
            runner.gameObject.SetActive(true);
            callBack();
            trigger.onTrigger = null;
        };
    }
}
