using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShaking : MonoBehaviour
{
    [SerializeField]
    private GameObject myCam;
    [SerializeField]
    private PlayerController playerCon;
    private bool isRun;
    private Vector3 shakePower;
    private int vibra;
    private float time;
    private Vector3 camOriPos;

    public float maxDistance = 0.2f;
    public float walkPower;
    public float runPower;
    public float walkTime;
    public float runTime;
    public int walkVibrato;
    public int runVibrato;
    public bool fadeOut;

    private void Awake()
    {
        if (playerCon == null)
            transform.GetComponent<PlayerController>();
    }

    private void Start()
    {
        camOriPos = myCam.transform.localPosition;

        myCam.SetActive(false);
        myCam.SetActive(true);

        StartCoroutine(Shaking());

        //Vector3 shakePower = new Vector3(0, power, 0);
        //myCam.transform.DOShakePosition(time, shakePower, vibrato, 90, false, fadeOut).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Vector3.Distance(Camera.main.transform.position, (myCam.transform.position)) >= maxDistance)
        {
            CameraOff();
            //Debug.Log(false);
            //Debug.LogWarning(Vector3.Distance(Camera.main.transform.position, myCam.transform.position));
        }

        //Debug.LogWarning(myCam.transform.localPosition + transform.position);
    }

    private void CameraOff()
    {
        myCam.transform.DOKill();
        myCam.transform.DOLocalMove(camOriPos, time / 2);
    }

    IEnumerator Shaking()
    {
        GameManager manager = GameManager.Instance;

        while (gameObject != null)
        {
            shakePower = new Vector3(0, walkPower, 0);
            vibra = walkVibrato;
            time = walkTime;

            if (playerCon._isMove && !DOTween.IsTweening(myCam.transform))
                myCam.transform.DOShakePosition(time, shakePower, vibra, 90, false, fadeOut).SetLoops(-1, LoopType.Yoyo);

            if (Input.GetButton("Run"))
            {
                vibra = runVibrato;
                time = runTime;
                shakePower = new Vector3(0, runPower, 0);

                if (!isRun)
                {
                    myCam.transform.DOKill();
                    myCam.transform.DOShakePosition(time, shakePower, vibra, 90, false, fadeOut).SetLoops(-1, LoopType.Yoyo);
                    isRun = true;
                }
            }
            else
            {
                if (isRun)
                {
                    isRun = false;
                    myCam.transform.DOKill();
                }
            }
                
            if (!playerCon._isMove)
                CameraOff();

            if (manager != null && GameManager.Instance.IsPause)
                CameraOff();

            yield return null;
        }

        Debug.Log("루틴 종료");
    }
}
