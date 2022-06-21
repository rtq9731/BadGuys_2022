using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class DestinationButterflyTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem triggerParticle = null;
    [SerializeField] ParticleSystem idleParticle = null;

    public event Action onTrigger;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        triggerParticle = GetComponentsInChildren<ParticleSystem>()[0];
        idleParticle = GetComponentsInChildren<ParticleSystem>()[1];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger?.Invoke();
            StartCoroutine(PlayParticle());
            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator PlayParticle()
    {
        idleParticle.Stop();
        triggerParticle.Play();
        yield return new WaitUntil(() => !triggerParticle.isPlaying);
        gameObject.SetActive(false);
    }
}
