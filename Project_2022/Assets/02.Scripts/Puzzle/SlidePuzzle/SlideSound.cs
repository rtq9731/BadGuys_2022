using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSound : MonoBehaviour
{
    [SerializeField] SoundScript sound;
    [SerializeField] AudioClip slideFX;

    private void Start()
    {
        sound = gameObject.GetComponent<SoundScript>();
    }

    public void SlideMove()
    {
        sound.audioSource.clip = slideFX;
        sound.audioSource.volume = 0.5f;
        sound.Play();
    }
}
