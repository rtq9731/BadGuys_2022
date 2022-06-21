using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public enum moveState
{ 
    Walk,
    Run,
    Stop
}

public class CameraShaking : MonoBehaviour
{
    [SerializeField]
    private GameObject myCam;
    [SerializeField]
    private PlayerController playerCon;
    private CinemachineBrain camBrain;
    private CinemachineVirtualCamera camVirtual;

    private moveState state;
    private Vector3 camOriPos;

    
    
    private Vector3 strength;

    public int vibra;
    public float walkPower;
    public float runPower;
    public float walkTime;
    public float runTime;

    private void Awake()
    {
        if (playerCon == null)
            transform.GetComponent<PlayerController>();

        camVirtual = myCam.GetComponent<CinemachineVirtualCamera>();
        camBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Start()
    {
        camOriPos = myCam.transform.localPosition;

        myCam.SetActive(false);
        myCam.SetActive(true);

        StartCoroutine(CameraShake());
    }

    private void WalkState(moveState cur)
    {
        if (state != cur)
        {
            myCam.transform.DOKill();
            strength = new Vector3(0, walkPower, 0);
            myCam.transform.DOShakePosition(walkTime, strength, vibra, 90, false, false).SetLoops(-1, LoopType.Yoyo);
        }

        state = cur;
    }

    private void RunState(moveState cur)
    {
        if (state != cur)
        {
            myCam.transform.DOKill();
            strength = new Vector3(0, runPower, 0);
            myCam.transform.DOShakePosition(runTime, strength, vibra, 90, false, false).SetLoops(-1, LoopType.Yoyo);
        }

        state = cur;
    }

    private void StopState()
    {
        myCam.transform.DOKill();
        myCam.transform.DOLocalMove(camOriPos, 0.1f);
        state = moveState.Stop;
    }

    IEnumerator CameraShake()
    {
        moveState curState = new moveState();
        curState = state;

        while (gameObject == true)
        {
            yield return null;

            if (playerCon._isMove)
            {
                curState = moveState.Walk;

                if (Input.GetButton("Run"))
                    curState = moveState.Run;
            }

            if (!playerCon._isMove)
                curState = moveState.Stop;

            if (GameManager.Instance.IsPause)
                curState = moveState.Stop;

            if (camBrain.IsLive(camVirtual) == false)
                curState = moveState.Stop;

            switch (curState)
            {
                case moveState.Walk:
                    if (state != curState)
                    {
                        myCam.transform.DOKill();
                        myCam.transform.DOLocalMove(camOriPos, 0.1f);
                        yield return new WaitForSeconds(0.1f);
                    }
                    WalkState(curState);
                    break;

                case moveState.Run:
                    if (state != curState)
                    {
                        myCam.transform.DOKill();
                        myCam.transform.DOLocalMove(camOriPos, 0.1f);
                        yield return new WaitForSeconds(0.1f);
                    }
                    RunState(curState);
                    break;

                case moveState.Stop:
                    StopState();
                    break;
            }
        }
    }
}
