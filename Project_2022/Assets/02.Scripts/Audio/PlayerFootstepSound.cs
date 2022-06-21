using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : PlayerColision
{
    [SerializeField] SoundScript[] footStepSounds;

    SoundScript curFootstepsSound;

    bool isPlaying = false;

    enum FloorType
    {
        Bridge,
        Sand,
        Dirt,
        Wood
    }

    public void SetPitch(float pitch)
    {
        foreach (var item in footStepSounds)
        {
            item.audioSource.pitch = pitch;
        }
    }

    protected override void OnTriggered(GameObject hit)
    {
        switch (hit.tag)
        {
            case "Bridge":
                ChangefootStepSound(FloorType.Bridge);
                break;
            case "Sand":
                ChangefootStepSound(FloorType.Sand);
                break;
            case "Dirt":
                ChangefootStepSound(FloorType.Dirt);
                break;
            case "Wood":
                ChangefootStepSound(FloorType.Wood);
                break;
            default:
                break;
        }
    }

    public void PauseSound()
    {
        isPlaying = false;
        curFootstepsSound.Stop();
    }

    public void PlaySound()
    {
        isPlaying = true;
        curFootstepsSound.SetLoop();
    }

    private void ChangefootStepSound(FloorType type)
    {
        if(isPlaying)
        {
            curFootstepsSound.Stop();
            curFootstepsSound.StopLoop();

            curFootstepsSound = footStepSounds[(int)(type)];

            curFootstepsSound.SetLoop();
        }
        else
        {
            curFootstepsSound = footStepSounds[(int)(type)];
        }
    }

    protected override void OnTriggeredExit(GameObject hit)
    {
        
    }
}
