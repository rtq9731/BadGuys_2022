using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationWaitpoint : MonoBehaviour
{
    [SerializeField] PlayerCollisionTrigger trigger = null;

    public bool isTriggered = false;

    private void Start()
    {
        trigger.onTrigger += () => isTriggered = true;
    }

    public void SetWait(RunnerAI runner, System.Action callBack)
    {
        runner.gameObject.SetActive(false);
        
        trigger.onTrigger += () =>
        {
            runner.gameObject.SetActive(true);
            callBack();
            trigger.onTrigger = null;
        };
    }
}
