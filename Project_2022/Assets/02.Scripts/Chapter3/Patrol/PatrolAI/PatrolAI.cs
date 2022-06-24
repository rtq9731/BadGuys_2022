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
    [SerializeField] PatrolDialog patrolDialog;

    //[HideInInspector]
    public AIStates _states = AIStates.Normal;

    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] Transform[] goOutDestinations;
    [SerializeField] Transform[] comeDestinations;

    [SerializeField] Transform chair;
    [SerializeField] GameObject player;
    [SerializeField] GameObject roomDoor;
    [SerializeField] Transform roomDoorTrm;

    [SerializeField] float initNormalTime = 15f;
    [SerializeField] float initComeInTime = 180f;

    [SerializeField] SoundScript chairSound;

    public float normalTime = 6f;
    public float comeInTime = 10f;

    public int destIndex = 0;

    public bool isArrive = true;

    Vector3 originChairPos;

    bool isMove = false;
    public bool isSit = true;

    float extraRotationSpeed = 5f;
    public float timingTime = 0f;

    

    bool isGoOut = true;
    bool isInRoom = true;
    bool isDetection = false;

    bool isOver = false;

    int endPatrolCount = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        originChairPos = chair.position;
        StartStates(AIStates.Normal);
    }

    private void Update()
    {
        if (GameManager.Instance.IsPause)
        {
            agent.isStopped = true;
            anim.speed = 0;
            return;
        }

        if(!GameManager.Instance.IsPause && agent.isStopped)
        {
            agent.isStopped = false;
            anim.speed = 1;
        }

        CheckStates();
        CheckPlayerOut();

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
                    if (normalTime >= initNormalTime && isMainAI == true)
                    {
                        patrolDialog.NormalDialogOn(endPatrolCount);
                    }
                    if(isArrive && otherAI.isArrive)
                    {
                    normalTime -= Time.deltaTime;

                    }
                    if (normalTime <= 0.1f)
                    {
                        StartStates(AIStates.GoOut);
                        normalTime = initNormalTime;
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
                        if (isMainAI == true)
                            patrolDialog.ComeInDialogOn(endPatrolCount);
                        StartStates(_states);
                    }
                }
                break;
            case AIStates.Detection:
                {
                    StartStates(_states);
                }
                break;
        }
    }

    void CheckPlayerOut()
    {
        if(isInRoom)
        {
            if (isDetection)
                return;

                if(!PatrolCheck.Instanse.IsHide() && isSit)
                {
                anim.SetTrigger("StandUp");
                float posZ = 0;

                if (transform.rotation.y > 0)
                {
                    posZ = 0.5f;
                    Debug.Log(transform.rotation.y);
                }
                else
                {
                    posZ = -0.5f;
                    Debug.Log(transform.rotation.y);
                }
                
                chairSound.Play();
                chair.transform.DOMoveZ(transform.position.z + posZ, 1.5f).OnComplete(() =>
                {
                    Debug.LogError("asdasddsad");
                    anim.SetBool("IsSitting", false);
                    _states = AIStates.Detection;
                    isArrive = false;
                    isSit = false;
                });
                isDetection = true;
            }

            if (!PatrolCheck.Instanse.IsHide() && !isSit)
            {
                _states = AIStates.Detection;
            }
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
                if (isMainAI == true)
                    patrolDialog.GoOutDialogOn(endPatrolCount);
                
                anim.SetTrigger("StandUp");
                float posZ = 0;

                if (transform.rotation.y > 0)
                {
                    posZ = 0.5f;
                    Debug.Log(transform.rotation.y);
                }
                else
                {
                    posZ = -0.5f;
                    Debug.Log(transform.rotation.y);
                }

                chairSound.Play();
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
                isArrive = true;
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

                    anim.SetBool("IsWalk", false);

                    isArrive = true;

                    return;
                }

                destIndex++;
                SetDestinations(destIndex, true);
            }
        }
    }

    void DoorAnimTimingGoOut(Transform[] destinations)
    {
        if (isMainAI)
        {
            if (timingTime >= 1.5f && !anim.GetBool("IsWalk") && destIndex == destinations.Length - 2)
            {
                FindObjectOfType<AiDoor>().OpenDoor();
                isMove = true;
                destIndex++;
                anim.SetBool("IsWalk", true);
                SetDestinations(destIndex, true);
               
            }

            if (Vector3.Distance(transform.position, agent.destination) <= 0.1f && destIndex == destinations.Length - 1)
            {
                FindObjectOfType<AiDoor>().CloseDoor();
                isInRoom = false;
                otherAI.isInRoom = false;
                anim.SetBool("IsWalk", false);
            }
        }
    }

    void DoorAnimTimingComeIn(Transform[] destinations)
    {
        if (timingTime >= 1.5f && !anim.GetBool("IsWalk") && destIndex == 1)
        {   
            if(isMainAI)
            {
                FindObjectOfType<AiDoor>().OpenDoor();
                
            }
            anim.SetBool("IsWalk", true);
            isMove = true;
            SetDestinations(destIndex, false);
            isInRoom = true;
            otherAI.isInRoom = true;
        }

        if (Vector3.Distance(transform.position, destinations[1].position) <= 0.1f && destIndex == 1)
        {
            if(isMainAI)
            {
                FindObjectOfType<AiDoor>().CloseDoor();
            }
            isGoOut = false;
            destIndex++;
            if (endPatrolCount == 3)
            {
                _states = AIStates.Detection;
                return;
            }
            endPatrolCount++;
            SetDestinations(destIndex, false);
        }

        if(Vector3.Distance(transform.position, destinations[2].position) <= 0.1f && destIndex == 2)
        {
            destIndex++;
            SetDestinations(destIndex, false);
        }
    }

    void OpenDoor()
    {
        if (isMainAI)
        {
            anim.SetTrigger("OpenDoor");
        }

        Debug.Log("asd");
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
                comeInTime = initComeInTime;
                isArrive = true;

                return;
            }

            DoorAnimTimingComeIn(comeDestinations);

            if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && isMove)
            {
                if (destIndex == 0)
                {
                    OpenDoor();
                    destIndex = 1;
                    isMove = false;
                    
                    
                    return;
                }

                if(destIndex == 1)
                    isArrive = false;

                if (destIndex >= 1)
                    return;

                destIndex++;
                SetDestinations(destIndex, false);
            }
        }
    }

    void DetectionPlayer()
    {
        isMove = true;
        if (isMainAI == true)
            patrolDialog.DetectionDialogOn();

        if (isMove && isInRoom)
        {
            agent.speed = 4f;

            if(!isMainAI && Vector3.Distance(transform.position, comeDestinations[1].position) <= 0.3f)
            {
                FindObjectOfType<AiDoor>().CloseDoor();
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= 1.5f && !isOver)
            {
                isOver = true;
                FindObjectOfType<PatrolCheck>().EndGame();
                agent.isStopped = true;
                Debug.Log("GameOver");
                isMove = false;
                return;
            }

            if (roomDoor.GetComponent<DoorLock>().isOpen)
            {
                agent.SetDestination(player.transform.position);
                anim.SetBool("IsWalk", true);
                return;
            }

            if (!roomDoor.GetComponent<DoorLock>().isOpen && PatrolCheck.Instanse.IsHide())
            {
                agent.SetDestination(roomDoorTrm.position);
                if(Vector3.Distance(transform.position, agent.destination) <= 0.1f)
                {
                    anim.SetBool("IsWalk", false);
                    roomDoor.GetComponent<Animator>().SetTrigger("IsOpen");
                    roomDoor.GetComponent<DoorLock>().isOpen = true;
                }
                return;
            }

            agent.SetDestination(player.transform.position);
            anim.SetBool("IsWalk", true);
        }
    }

    void StandToSit()
    {
        anim.SetTrigger("SitDown");
        anim.SetBool("IsWalk", false);
        isMove = false;
        isSit = true;
        chair.transform.DOMoveZ(originChairPos.z, 1.5f);

        chairSound.Play();

        if (isMainAI)
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