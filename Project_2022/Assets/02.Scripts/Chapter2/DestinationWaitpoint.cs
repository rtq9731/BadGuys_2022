using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationWaitpoint : MonoBehaviour
{
    [SerializeField] PlayerCollisionTrigger trigger = null;

    RunnerAI runner = null;

    private void Start()
    {
        runner = FindObjectOfType<RunnerAI>(true);
    }

    public void SetWait(System.Action callBack)
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
