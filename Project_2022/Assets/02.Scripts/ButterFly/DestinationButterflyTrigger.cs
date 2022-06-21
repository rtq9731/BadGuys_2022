using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class DestinationButterflyTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem idleParticle = null;

    [SerializeField] float flySpeed;
    [SerializeField] Animator butterflyAnim = null;
    [SerializeField] Transform dest = null;

    public event Action onTrigger;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
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

        butterflyAnim.SetBool("isTriggered", true);

        Transform butterfly = butterflyAnim.transform;
        float distToDest = Vector2.Distance(dest.position, butterfly.position);

        while (Vector2.Distance(dest.position, butterfly.position) >= 0.01f)
        {
            butterfly.LookAt(dest);
            butterfly.Translate((dest.position - butterfly.position).normalized * flySpeed * Time.deltaTime);

            foreach (var item in butterflyAnim.GetComponentsInChildren<MeshRenderer>())
            {
                item.material.SetFloat("_NoiseStrength", Mathf.Lerp(0, 300, Vector2.Distance(dest.position, butterfly.position) / distToDest));
            }
            yield return null;
        }



        gameObject.SetActive(false);
    }
}
