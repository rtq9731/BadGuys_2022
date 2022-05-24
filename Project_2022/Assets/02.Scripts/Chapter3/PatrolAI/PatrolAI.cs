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
                    // ���� ���Դٸ� �ٽ� �ɾƹ����� 
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
        Debug.Log("�ɱ�");
        //
    }

    void GoOut()
    {
        Debug.Log("������ ������");
        //������ ������
    }

    void ComeIn()
    {
        Debug.Log("������ ������");
        //������ ������
    }

    void DetectionPlayer()
    {
        Debug.Log("�÷��̾� �߰����� �� �ൿ");
        //�÷��̾� �߰����� �� �ൿ 
    }
}
