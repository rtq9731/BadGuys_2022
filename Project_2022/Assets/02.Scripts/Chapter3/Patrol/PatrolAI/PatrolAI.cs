using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum AIStates
{
    Normal,
    GoOut,
    ComeIn,
    Detection
}

public enum Kinds
{
    Man1 = 0,
    Man2 = 1,
}


public class PatrolAI : MonoBehaviour
{
    [HideInInspector]
    public AIStates _states = AIStates.Normal;

    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] Transform[] normalDestinations;
    [SerializeField] Transform[] goOutDestinations;
    [SerializeField] Transform[] comeDestinations;

    [SerializeField] Transform chair;

    public float normalTime = 6f;
    public float comeInTime = 10f;

    public int destIndex = 0;

    Vector3 originChairPos;


    bool isMove = false;
    bool isSit = true;

    float extraRotationSpeed = 5f;
    public float timingTime = 0f;


    bool isGoOut = true;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        originChairPos = chair.position;
        StartStates(AIStates.Normal);
    }

    private void Update()
    {
        CheckStates();

        timingTime += Time.deltaTime;

        if (isMove)
        {
            Vector3 lookrotation = agent.steeringTarget - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);
        }
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
                    StartStates(_states);
                    
                }
                break;
            case AIStates.ComeIn:
                {
                    comeInTime -= Time.deltaTime;
                    isMove = false;

                    if (comeInTime <= 0)
                    {
                        if (destIndex == goOutDestinations.Length -1 )
                            destIndex = -1;
                        isMove = true;
                        
                        StartStates(_states);
                    }
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
        anim.SetBool("IsSitting", true);
    }

    void GoOut()
    {
        //일단 일어나고 
        if(isGoOut)
        {
            if (isSit)
            {
                anim.SetTrigger("StandUp");
                chair.transform.DOMoveZ(transform.position.z - 0.5f, 1.5f).OnComplete(() =>
                {
                    SetDestinations(0, true);
                    anim.SetBool("IsSitting", false);
                });
                isSit = false;
            }

            DoorAnimTimingGoOut(goOutDestinations);

            if (Vector3.Distance(transform.position, goOutDestinations[goOutDestinations.Length - 1].position) <= 0.1f)
            {
                _states = AIStates.ComeIn;
                isGoOut = false;
                isSit = true;
                return;
            }

            if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && isMove)
            {
                if (destIndex == goOutDestinations.Length - 2)
                {
                    OpenDoor();
                    isMove = false;
                    return;
                }

                if (destIndex >= goOutDestinations.Length - 2)
                    return;
                destIndex++;
                SetDestinations(destIndex, true);
            }
        }
    }

    void DoorAnimTimingGoOut(Transform[] destinations)
    {
        if(timingTime >= 2.3f && !anim.GetBool("IsWalk") && destIndex == destinations.Length - 2)
        {
            FindObjectOfType<AiDoor>().OpenDoor();
            isMove = true;
            destIndex++;
            anim.SetBool("IsWalk", true);
            SetDestinations(destIndex , true);
            Debug.Log("문 열기");
        }                                                               
        
        if(Vector3.Distance(transform.position, agent.destination) <= 0.1f && destIndex == destinations.Length - 1)
        {
            FindObjectOfType<AiDoor>().CloseDoor(); 
            anim.SetBool("IsWalk", false);
            isGoOut = false;
            Debug.Log("문 닫기");
        }
    }

    void DoorAnimTimingComeIn(Transform[] destinations)
    {
        if (timingTime >= 2.3f && !anim.GetBool("IsWalk") && destIndex == 1)
        {
            FindObjectOfType<AiDoor>().OpenDoor();
            isMove = true;
            destIndex++;
            anim.SetBool("IsWalk", true);
            SetDestinations(destIndex, false);
        }

        if (Vector3.Distance(transform.position, destinations[1].position) <= 0.1f)
        {
            FindObjectOfType<AiDoor>().CloseDoor();
            isGoOut = false;
            SetDestinations(destIndex, false);
            Debug.Log("asd");
        }
    }

    void OpenDoor()
    {
        anim.SetBool("IsWalk", false);
        anim.SetTrigger("OpenDoor");
        timingTime = 0f;
    }

    void ComeIn()
    {
        if(!isGoOut)
        {

            if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && destIndex == 2)
            {
                StandToSit();
                isGoOut = true;
                destIndex = 0;
                comeInTime = 10f;
                Debug.LogError("앉아요");

                return;
            }

            DoorAnimTimingComeIn(comeDestinations);

            if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && isMove)
            {
                if (destIndex == 0)
                {
                    OpenDoor();
                    destIndex =1;
                    isMove = false;
                    return;
                }


                if (destIndex >= 1)
                    return;

                destIndex++;
                SetDestinations(destIndex, false);

            }

            
        }
    }

    void DetectionPlayer()
    {
        Debug.Log("플레이어 발각했을 때 행동");
        //플레이어 발각했을 때 행동 
    }

    void StandToSit()
    {
        anim.SetTrigger("SitDown");
        anim.SetBool("IsWalk", false);
        isMove = false;
        chair.transform.DOMoveZ(originChairPos.z, 1.5f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        StartStates(AIStates.Normal);
    }

    void SetDestinations(int idx, bool isGoOut)
    {
        isMove = true;

        anim.SetBool("IsWalk", true);

        if (isGoOut)
            agent.SetDestination(goOutDestinations[idx].position);
        else
            agent.SetDestination(comeDestinations[idx].position);

    }
}