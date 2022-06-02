using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerAI : MonoBehaviour
{
    NavMeshAgent ai = null;
    Animator anim = null;
    [SerializeField] TimelineDestination[] destinations = null;

    Queue<Action> arriveActQueue = new Queue<Action>();

    float initSpeed = 0f;

    Transform player = null;

    int runningHash = 0;
    bool isArrived = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>(true).transform;
        initSpeed = ai.speed;

        runningHash = Animator.StringToHash("isRunning");

    }
    private void Start()
    {
        SetDestination(0);
    }

    private void Update()
    {
        anim.SetBool(runningHash, !ai.isStopped);

        ai.speed = GameManager.Instance.IsPause ? 0 : initSpeed;

        // Debug.Log((Vector3.Distance(ai.destination, transform.position)) <= 0.1f);
        if (Vector3.Distance(ai.destination, transform.position) <= 2f)
        {
            ai.isStopped = true;

            if (!isArrived)
            {
                isArrived = true;

                if(arriveActQueue.Count > 0)
                {
                    arriveActQueue.Dequeue()?.Invoke();
                }
            }
        }
    }
    
    public void SetPos(int idx)
    {
        ai.transform.position = destinations[idx].destination.position;
    }

    public void SetDestination(int idx)
    {
        gameObject.SetActive(true);
        isArrived = false;

        ai.SetDestination(destinations[idx].destination.position);
        ai.isStopped = false;

        if (idx == destinations.Length - 1)
        {
            arriveActQueue.Enqueue(() => gameObject.SetActive(false));
        }

        DestinationWaitpoint waitpoint = destinations[idx].destination.GetComponent<DestinationWaitpoint>();
        
        if (waitpoint)
        {
            if(waitpoint.isTriggered)
            {
                SetDestination(idx + 1);
                return;
            }

            Debug.Log(waitpoint.gameObject);
            arriveActQueue.Enqueue(() =>
            {
                waitpoint.SetWait(this, () => SetDestination(idx + 1));
            });
            return;
        }


        if (destinations[idx].timeLine != null)
        {
            arriveActQueue.Enqueue(() =>
            {
                ai.gameObject.SetActive(false);
                destinations[idx].timeLine.SetTrigger(() =>
                {
                    SetDestination(idx + 2);
                    SetPos(idx + 1);
                });
            });
        }
        else
        {
            arriveActQueue.Enqueue(() =>
            {
                SetDestination(idx + 1);
            });
        }
    }
}

[Serializable]
class TimelineDestination
{
    public RunnerAITimelineTrigger timeLine = null;
    public Transform destination = null;
}

