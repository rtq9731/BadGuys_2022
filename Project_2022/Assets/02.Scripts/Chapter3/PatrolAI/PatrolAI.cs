using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIStates
{
    Normal,
    GoOut,
    ComeIn,
    Detection
}


public class PatrolAI : MonoBehaviour
{
    RunnerAI ai;

    [HideInInspector]
    public AIStates _states = AIStates.Normal;

    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] Transform[] normalDestinations;
    [SerializeField] Transform[] goOutDestinations;
    [SerializeField] Transform[] come;

    [SerializeField] Transform chair;

    public float normalTime = 6f;
    public float goOutTime = 10f;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        StartStates(AIStates.Normal);
    }

    private void Update()
    {
        CheckStates();
    }

    void StartStates(AIStates states)
    {
        _states = states;
        switch (states)
        {
            case AIStates.Normal:
                Normal();
                break;
            case AIStates.GoOut:
                GoOut();
                break;
            case AIStates.ComeIn:
                ComeIn();
                break;
            case AIStates.Detection:
                DetectionPlayer();
                break;
        }
    }

    void CheckStates()
    {
        switch (_states)
        {
            case AIStates.Normal:
                {
                    normalTime -= Time.deltaTime;
                    if (normalTime <= 0)
                    {
                        StartStates(AIStates.GoOut);
                        normalTime = 5;
                    }
                }
                break;
            case AIStates.GoOut:
                {
                    goOutTime -= Time.deltaTime;
                    if (goOutTime <= 0)
                    {
                        StartStates(AIStates.ComeIn);
                        goOutTime = 10f;
                    }
                }
                break;
            case AIStates.ComeIn:
                {
                    // 만약 들어왔다면 다시 앉아버리기 
                    //StartStates(AIStates.Normal);
                }
                break;
            case AIStates.Detection:
                {

                }
                break;
        }
    }

    void Normal()
    {
        Debug.Log("앉기");
        anim.SetBool("IsSitting", true);
    }

    void GoOut()
    {
        //일단 일어나고 
        anim.SetTrigger("StandUp");


        anim.SetBool("IsWalk", true);
        anim.SetBool("IsSitting", false);
    }

    void ComeIn()
    {
        Debug.Log("안으로 들어오기");
        anim.SetBool("IsWalk", true);
        //안으로 들어오기

        //앉기

        if (Vector3.Distance(transform.position, chair.position) <= 0.1f)
        {
            anim.SetTrigger("SitDown");
            anim.SetBool("IsWalk", false);
        }
    }

    void DetectionPlayer()
    {
        Debug.Log("플레이어 발각했을 때 행동");
        //플레이어 발각했을 때 행동 
    }
}
