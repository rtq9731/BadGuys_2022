using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseKidnapperAI : MonoBehaviour
{
    NavMeshAgent ai = null;
    Animator anim = null;
    [SerializeField] Transform[] destinations = null;

    Queue<Action> callBackQueue = new Queue<Action>();

    int runningHash = 0;
    bool isArrived = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();

        runningHash = Animator.StringToHash("isRunning");

        SetDestination(0, () =>
        {
            gameObject.SetActive(false);
            transform.position = destinations[1].position;
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        });
    }

    private void Update()
    {
        anim.SetBool(runningHash, !ai.isStopped);

        if(Vector3.Distance(ai.destination, transform.position) <= 0.1f)
        {
            ai.isStopped = true;

            if (!isArrived)
            {
                isArrived = true;
                if (callBackQueue.Count > 0)
                {
                    callBackQueue.Dequeue()?.Invoke();
                }
            }
        }
    }
    
    public void SetPos(int idx)
    {
        ai.transform.position = destinations[idx].position;
    }

    public void SetDestination(int idx, Action callBack)
    {
        gameObject.SetActive(true);
        ai.SetDestination(destinations[idx].position);
        callBackQueue.Enqueue(callBack);
        isArrived = false;
        ai.isStopped = false;
    }
}
