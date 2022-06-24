using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ps;
    [SerializeField]
    private SoundScript ss;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        ss = GetComponent<SoundScript>();
    }

    public void ParticleOn()
    {
        ss.Play();
        Invoke("PSPlay", 0.5f);
    }

    private void PSPlay()
    {
        ps.Play();
    }
}
