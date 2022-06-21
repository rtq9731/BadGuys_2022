using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSound : PlayerColision
{
    [SerializeField] SoundScript[] footStepSounds;

    SoundScript curFootstepsSound;
    FloorType curFloor = FloorType.Bridge;

    bool isPlaying = false;

    enum FloorType
    {
        Bridge,
        Sand,
        Dirt,
        Wood,
        Other
    }

    public void SetPitch(float pitch)
    {
        foreach (var item in footStepSounds)
        {
            if(item.audioSource != null)
            item.audioSource.pitch = pitch;
        }
    }

    protected override void OnTriggered(GameObject hit)
    {
        if(curFloor != FloorType.Other && hit.CompareTag(Enum.GetName(typeof(FloorType), curFloor)))
        {
            return;
        }

        switch (hit.tag)
        {
            case "Bridge":
                ChangefootStepSound(FloorType.Bridge);
                curFloor = FloorType.Bridge;
                break;
            case "Sand":
                ChangefootStepSound(FloorType.Sand);
                curFloor = FloorType.Sand;
                break;
            //case "Dirt":
            //    ChangefootStepSound(FloorType.Dirt);
            //    break;
            case "Wood":
                ChangefootStepSound(FloorType.Wood);
                curFloor = FloorType.Wood;
                break;
            default:
                PauseSound();
                curFloor = FloorType.Other;
                break;
        }
    }

    public void PauseSound()
    {
        isPlaying = false;
        curFootstepsSound?.Stop();
    }

    public void PlaySound()
    {
        if(isPlaying)
        {
            return;
        }

        isPlaying = true;
        curFootstepsSound.SetLoop(true);
        curFootstepsSound.Play();
    }

    private void ChangefootStepSound(FloorType type)
    {
        if(isPlaying)
        {
            curFootstepsSound.Stop();
            curFootstepsSound.SetLoop(false);

            curFootstepsSound = footStepSounds[(int)(type)];
            Debug.Log(curFootstepsSound);

            PlaySound();
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
