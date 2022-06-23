using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESound : MonoBehaviour
{
    AudioSource audioSource;

    public SoundScript successSound;
    public SoundScript failedSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SuccessQTE()
    {
        audioSource.clip = successSound.clip;
        audioSource.Play();
    }

    public void FailedQTE()
    {
        audioSource.clip = failedSound.clip;
        audioSource.Play();
    }
}
