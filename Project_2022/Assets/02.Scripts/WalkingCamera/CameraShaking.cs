using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using DG.Tweening;

public enum moveState
{ 
    Walk,
    Run,
    Stop
}

[Serializable]
public class ShakeData
{
    public FloorType floor;

    public int vibra;
    public float walkPower;
    public float runPower;
    public float walkTime;
    public float runTime;
}

public class CameraShaking : MonoBehaviour
{
    [SerializeField]
    private GameObject myCam;
    [SerializeField]
    private PlayerController playerCon;
    [SerializeField]
    private PlayerFootstepSound playerStep;
    private CinemachineBrain camBrain;
    private CinemachineVirtualCamera camVirtual;

    private moveState state;
    private Vector3 camOriPos;
    private Vector3 strength;
    private int floorNum;

    public List<ShakeData> dataList = new List<ShakeData>(5);

    private int vibra;
    private float walkPower;
    private float runPower;
    private float walkTime;
    private float runTime;

    private void Awake()
    {
        if (playerCon == null)
            transform.GetComponent<PlayerController>();
        
        if (playerStep == null)
            transform.GetComponent<PlayerFootstepSound>();

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

    private void WalkState(moveState cur, int floor)
    {
        if (state != cur || floorNum != floor)
        {
            myCam.transform.DOKill();
            ShakeValueSetting(floor);
            strength = new Vector3(0, walkPower, 0);
            myCam.transform.DOShakePosition(walkTime, strength, vibra, 90, false, false).SetLoops(-1, LoopType.Yoyo);
        }

        state = cur;
    }

    private void RunState(moveState cur, int floor)
    {
        if (state != cur || floorNum != floor)
        {
            myCam.transform.DOKill();
            ShakeValueSetting(floor);
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

    private int FloorCheck()
    {
        FloorType cur = playerStep.curFloor;

        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].floor == cur)
            {
                return i;
            }
        }

        return dataList.Count - 1;
    }

    private void ShakeValueSetting(int cur)
    {
        ShakeData data = dataList[cur];

        vibra = data.vibra;
        walkPower = data.walkPower;
        runPower = data.runPower;
        walkTime = data.walkTime;
        runTime = data.runTime;
    }

    IEnumerator CameraShake()
    {
        moveState curState = new moveState();
        curState = state;
        int curFloor = 0;

        while (gameObject == true)
        {
            yield return null;

            if (playerCon._isMove)
            {
                curState = moveState.Walk;

                if (Input.GetButton("Run"))
                    curState = moveState.Run;
            }

            if (playerCon._isMove == false)
                curState = moveState.Stop;

            if (GameManager.Instance.IsPause)
                curState = moveState.Stop;

            if (camBrain.IsLive(camVirtual) == false)
                curState = moveState.Stop;

            curFloor = FloorCheck();

            switch (curState)
            {
                case moveState.Walk:
                    if (state != curState)
                    {
                        myCam.transform.DOKill();
                        myCam.transform.DOLocalMove(camOriPos, 0.1f);
                        yield return new WaitForSeconds(0.1f);
                    }
                    WalkState(curState, curFloor);
                    break;

                case moveState.Run:
                    if (state != curState)
                    {
                        myCam.transform.DOKill();
                        myCam.transform.DOLocalMove(camOriPos, 0.1f);
                        yield return new WaitForSeconds(0.1f);
                    }
                    RunState(curState, curFloor);
                    break;

                case moveState.Stop:
                    StopState();
                    break;
            }

            floorNum = curFloor;
        }
    }
}
