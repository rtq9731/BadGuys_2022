using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAccident : MonoBehaviour
{
    [SerializeField] SoundScript sound;

    private void Start()
    {
        sound = transform.GetComponent<SoundScript>();
    }

    public void SoundOn()
    {
        sound.SetLoop(true);
        sound.audioSource.volume = 0.3f;
        sound.Play();
    }

    public void SoundOff()
    {
        sound.Pause();
    }
}
