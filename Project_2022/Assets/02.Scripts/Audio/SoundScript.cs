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

    public bool isPause = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.playOnAwake = false;
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        originVolume = audioSource.volume;
        audioSource = GetComponent<AudioSource>();
    }

    public void SetLoop(bool loop)
    {
        audioSource.loop = loop;
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
        audioSource?.Stop();
    }
}

public enum AudioType
{
    Other,
    BGM,
    SFX,
}

