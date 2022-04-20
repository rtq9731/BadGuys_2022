using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorGathering : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.VelocityOverLifetimeModule psVel;
    
    public float speed = 5f;
    public float followWaittingTime = 1f;
    public float particleLiveTime = 2f;
    public Color particleColor;
    public GameObject testTarget;

    public float randomSizeMin;
    public float randomSizeMax;

    private GameObject target;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        psVel = ps.velocityOverLifetime;
        ParticleSetting();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            ParticleOn(testTarget);
        }
#endif
    }

    private void ParticleSetting()
    {
        ps.startColor = particleColor;
        ps.startLifetime = particleLiveTime;
        ps.startSpeed = 0f;
        psVel.radial = -speed;
    }

    public void ParticleOn(GameObject target)
    {
        ps.Play();
        StartCoroutine(ParticleMove(target));
    }

    IEnumerator ParticleMove(GameObject target)
    {
        Debug.Log("start");

        while (ps.isPlaying)
        {
            //Vector3 targetPos = target.transform.position;
            Vector3 targetPos = target.transform.position - transform.position;
            psVel.orbitalOffsetX = targetPos.x;
            psVel.orbitalOffsetY = targetPos.y;
            psVel.orbitalOffsetZ = targetPos.z;

            yield return null;
        }

        Debug.Log("end");
    }
}

