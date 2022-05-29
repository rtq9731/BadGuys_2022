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


public class PatrolAI : MonoBehaviour
{
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

    private int destIndex = 0;

    Vector3 originChairPos;


    bool isMove = false;
    bool isSit = true;

    float extraRotationSpeed = 5f;

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

        if (isMove)
        {
            Vector3 lookrotation = agent.steeringTarget - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);
        }


        if (Vector3.Distance(agent.destination, transform.position) <= 0.1f && isMove)
        {
            if (destIndex == goOutDestinations.Length - 1)
            {
                Debug.LogError("���߱�");

                anim.SetBool("IsWalk", false);

                isMove = false;
                return;
            }
            destIndex++;
            SetDestinations(destIndex);
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
                    goOutTime -= Time.deltaTime;
                    if (goOutTime <= 0)
                    {
                        //StartStates(AIStates.ComeIn);
                        goOutTime = 10f;
                    }
                }
                break;
            case AIStates.ComeIn:
                {

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
        Debug.Log("�ɱ�");
        anim.SetBool("IsSitting", true);
    }

    void GoOut()
    {
        //�ϴ� �Ͼ�� 
        if (isSit)
        {
            anim.SetTrigger("StandUp");
            chair.transform.DOMoveZ(transform.position.z - 0.5f, 1.5f).OnComplete(() =>
            {
                Debug.LogWarning("asdasdasdasdasdasd");
                SetDestinations(0);
                anim.SetBool("IsSitting", false);
            });
            isSit = false;
        }

        if (Vector3.Distance(transform.position, goOutDestinations[goOutDestinations.Length - 1].position) <= 0.1f)
        {
            Debug.LogWarning("������");
            FindObjectOfType<AiDoor>().OpenDoor();
        }
    }

    void ComeIn()
    {
        Debug.Log("������ ������");
        //������ ������

        //�ɱ�
        StandToSit();

        if (Vector3.Distance(transform.position, goOutDestinations[0].position) <= 0.1f)
        {
            StandToSit();
        }
    }

    void DetectionPlayer()
    {
        Debug.Log("�÷��̾� �߰����� �� �ൿ");
        //�÷��̾� �߰����� �� �ൿ 
    }

    void StandToSit()
    {
        anim.SetTrigger("SitDown");
        anim.SetBool("IsWalk", false);

        chair.transform.DOMoveZ(originChairPos.z, 1.5f);

        StartStates(AIStates.Normal);
    }


    void SetDestinations(int idx)
    {
        isMove = true;

        anim.SetBool("IsWalk", true);

        agent.SetDestination(goOutDestinations[idx].position);
    }

}