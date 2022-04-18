using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorGathering : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;
    
    public float speed = 5f;
    public float followWaittingTime = 1f;
    public float particleLiveTime = 2f;
    public Color particleColor;
    public GameObject testTarget;

    private GameObject target;
    private int particleCount;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        ParticleSetting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ParticleOn(testTarget);
        }
    }

    private void ParticleSetting()
    {
        ps.startColor = particleColor;
        ps.startLifetime = particleLiveTime;
    }

    public void ParticleOn(GameObject target)
    {
        ps.Play();
        
        //ps.velocityOverLifetime.orbitalOffsetX = ParticleSystem.VelocityOverLifetimeModule
        //ps.playbackSpeed;
        //ps.DOPlayBackwards();
        //ps.DORewind();
        //StartCoroutine(ParticleMove());
    }

    IEnumerator ParticleMove()
    {
        yield return new WaitForSeconds(followWaittingTime);
        Debug.Log("start");

        while (ps.isPlaying)
        {
            if (true)
            {
                if (particles == null)
                    particles = new ParticleSystem.Particle[ps.main.maxParticles];

                particleCount = ps.GetParticles(particles);
                float step = speed * Time.deltaTime;

                for (int i = 0; i < particleCount; i++)
                {
                    Debug.LogWarning(i);
                    particles[i].position = Vector3.MoveTowards(particles[i].position, target.transform.position, step);
                }

                ps.SetParticles(particles, particleCount);
            }

            yield return null;
        }

        Debug.Log("end");
    }
}

