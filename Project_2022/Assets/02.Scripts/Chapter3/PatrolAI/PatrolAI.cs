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
    
    Transform[] normalDestinations;
    Transform[] goOutDestinations;
    Transform[] come; 

    public float normalTime = 10f;
    public float goOutTime = 30f;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

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
                        normalTime = 10f;
                    }
                }
                break;
            case AIStates.GoOut:
                {
                    goOutTime -= Time.deltaTime;
                    if (goOutTime <= 0)
                    {
                        StartStates(AIStates.ComeIn);
                        goOutTime = 30f;
                    }
                }
                break;
            case AIStates.ComeIn:
                {
                    // 만약 들어왔다면 다시 앉아버리기 
                    StartStates(AIStates.Normal);
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
        //
    }

    void GoOut()
    {
        Debug.Log("밖으로 나가기");
        //밖으로 나가기
    }

    void ComeIn()
    {
        Debug.Log("안으로 들어오기");
        //안으로 들어오기
    }

    void DetectionPlayer()
    {
        Debug.Log("플레이어 발각했을 때 행동");
        //플레이어 발각했을 때 행동 
    }
}
