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
    [SerializeField] bool isMainAI;
    [SerializeField] PatrolAI otherAI;

    [HideInInspector]
    public AIStates _states = AIStates.Normal;

    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] Transform[] goOutDestinations;
    [SerializeField] Transform[] comeDestinations;

    [SerializeField] Transform chair;

    public float normalTime = 6f;
    public float comeInTime = 10f;

    public int destIndex = 0;

    public bool isArrive = true;

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

        if (isMove && agent.steeringTarget != null)
        {
            Vector3 lookrotation = agent.steeringTarget - transform.position;

            if (lookrotation == Vector3.zero)
                return;
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
                    if(isArrive && otherAI.isArrive)
                    {
                        normalTime -= Time.deltaTime;
                    }
                    if (normalTime <= 0.1f)
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
                    isMove = false;
                    if (isArrive && otherAI.isArrive)
                    {
                        comeInTime -= Time.deltaTime;
                    }

                    if (comeInTime <= 0.1f)
                    {
                        if (destIndex == goOutDestinations.Length - 1)
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
        if (isGoOut)
        {
            
            if (isSit)
            {
                anim.SetTrigger("StandUp");
                float posZ = 0;

                if (transform.rotation.y > 0)
                {
                    posZ = 0.5f;
                    Debug.Log("에엑");
                    Debug.Log(transform.rotation.y);
                }
                else
                {
                    posZ = -0.5f;
                    Debug.Log("이익");
                    Debug.Log(transform.rotation.y);
                }


                chair.transform.DOMoveZ(transform.position.z + posZ, 1.5f).OnComplete(() =>
                {
                    SetDestinations(0, true);
                    anim.SetBool("IsSitting", false);
                    isArrive = false;
                });
                isSit = false;

            }

            DoorAnimTimingGoOut(goOutDestinations);

            if (Vector3.Distance(transform.position, goOutDestinations[goOutDestinations.Length - 1].position) <= 0.1f)
            {
                _states = AIStates.ComeIn;
                isGoOut = false;
                isSit = true;
                isArrive = true;
                Debug.LogError("멈춰");
                return;
            }

            if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && isMove)
            {
                if (destIndex == goOutDestinations.Length - 2 && isMainAI)
                {
                    OpenDoor();
                    isMove = false;
                    return;
                }

                if (destIndex > goOutDestinations.Length - 2 && isMainAI)
                    return;

                if (destIndex == goOutDestinations.Length - 1)
                {
                    _states = AIStates.ComeIn;
                    isGoOut = false;
                    isSit = true;

                    anim.SetBool("IsWalk", false);
                    Debug.LogError("멈춰");

                    isArrive = true;

                    return;
                }

                destIndex++;
                SetDestinations(destIndex, true);
                Debug.LogError("asd");
            }
        }
    }

    void DoorAnimTimingGoOut(Transform[] destinations)
    {
        if (isMainAI)
        {
            if (timingTime >= 2.3f && !anim.GetBool("IsWalk") && destIndex == destinations.Length - 2)
            {
                FindObjectOfType<AiDoor>().OpenDoor();
                isMove = true;
                destIndex++;
                anim.SetBool("IsWalk", true);
                SetDestinations(destIndex, true);
                Debug.Log("문 열기");
            }

            if (Vector3.Distance(transform.position, agent.destination) <= 0.1f && destIndex == destinations.Length - 1)
            {
                FindObjectOfType<AiDoor>().CloseDoor();
                anim.SetBool("IsWalk", false);
                Debug.Log("문 닫기");
            }
        }
    }

    void DoorAnimTimingComeIn(Transform[] destinations)
    {
        if (isMainAI)
        {
            if (timingTime >= 2.3f && !anim.GetBool("IsWalk") && destIndex == 1)
            {
                FindObjectOfType<AiDoor>().OpenDoor();
                isMove = true;
                SetDestinations(destIndex, false);
                anim.SetBool("IsWalk", true); 
            }

            if (Vector3.Distance(transform.position, destinations[1].position) <= 0.1f && destIndex == 1)
            {
                FindObjectOfType<AiDoor>().CloseDoor();
                isGoOut = false;
                destIndex++;
                SetDestinations(destIndex, false);
                Debug.Log("문을 닫아보아요");
            }
        }

    }

    void OpenDoor()
    {
        if (isMainAI)
            anim.SetTrigger("OpenDoor");

        anim.SetBool("IsWalk", false);
        timingTime = 0f;
    }

    void ComeIn()
    {
        if (!isGoOut)
        {

            if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && destIndex == comeDestinations.Length - 1)
            {
                StandToSit();
                isGoOut = true;
                destIndex = 0;
                comeInTime = 10f;
                isArrive = true;
                Debug.LogError("앉아요");


                return;
            }

            DoorAnimTimingComeIn(comeDestinations);

            if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && isMove)
            {
                if (destIndex == 0 && isMainAI)
                {
                    OpenDoor();
                    destIndex = 1;
                    isMove = false;
                    return;
                }


                if (destIndex >= 1 && isMainAI)
                    return;

                if(destIndex == 1)
                    isArrive = false;

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

        if(isMainAI)
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.5f);

        }
        else
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.5f);
        }

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