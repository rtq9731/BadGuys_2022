using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundScript : MonoBehaviour
{
    public float originVolume = 0f;
    public AudioSource audioSource = null;
    public AudioType audioType = AudioType.Other;

    public AudioClip clip = null;

    Coroutine loopCor;

    bool isLoop = false;
    bool isPause = false;

    private void Awake()
    {
        audioSource.playOnAwake = false;
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        originVolume = audioSource.volume;
        audioSource = GetComponent<AudioSource>();
    }

    public void SetLoop()
    {
        isLoop = true;
        loopCor = StartCoroutine(Soundloop());
    }

    public void StopLoop()
    {
        if(loopCor != null)
        {
            StopCoroutine(loopCor);
        }

        isLoop = false;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
        isPause = true;
    }

    public void Resume()
    {
        audioSource.UnPause();
        isPause = false;
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    IEnumerator Soundloop()
    {
        while(isLoop)
        {
            if(isPause)
            {
                yield return null;
            }

            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            yield return new WaitUntil(() => !audioSource.isPlaying);
        }
    }
}

public enum AudioType
{
    Other,
    BGM,
    SFX,
}

