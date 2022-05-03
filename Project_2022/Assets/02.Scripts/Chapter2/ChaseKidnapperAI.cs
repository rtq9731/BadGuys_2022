using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseKidnapperAI : MonoBehaviour
{
    NavMeshAgent ai = null;
    Animator anim = null;
    [SerializeField] Transform destination = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ai = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        anim.SetBool(0, !ai.isStopped);

        if (ai.destination != destination.position)
        {
            ai.SetDestination(destination.position);
        }
    }
}
