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
        InvokeRepeating("SoundRepeating", 0, 3f);
    }

    public void SoundRepeating()
    {
        sound.Play();
    }

    public void SoundOff()
    {
        sound.Pause();
    }
}
