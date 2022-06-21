using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FloorType
{
    Bridge,
    Sand,
    Dirt,
    Wood,
    Asphalt,
    Other
}

public class PlayerFootstepSound : PlayerColision
{
    [SerializeField] SoundScript[] footStepSounds;

    SoundScript curFootstepsSound;
    public FloorType curFloor = FloorType.Bridge;

    bool isPlaying = false;

    private void Awake()
    {
        GameManager.Instance._onPauseChanged += CheckPauseSound; 
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
                PauseSound();
                ChangefootStepSound(FloorType.Bridge);
                curFloor = FloorType.Bridge;
                break;
            case "Sand":
                PauseSound();
                ChangefootStepSound(FloorType.Sand);
                curFloor = FloorType.Sand;
                break;
            //case "Dirt":
            //PauseSound();
            //    ChangefootStepSound(FloorType.Dirt);
            //    break;
            case "Wood":
                PauseSound();
                ChangefootStepSound(FloorType.Wood);
                curFloor = FloorType.Wood;
                break;
            case "Asphalt":
                PauseSound();
                ChangefootStepSound(FloorType.Asphalt);
                curFloor = FloorType.Asphalt;
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

    private void CheckPauseSound(bool pause)
    {
        if (pause)
            PauseSound();
    }

    public void PlaySound()
    {
        if(isPlaying)
        {
            return;
        }

        isPlaying = true;
        curFootstepsSound?.SetLoop(true);
        curFootstepsSound?.Play();
    }

    private void ChangefootStepSound(FloorType type)
    {
        if(isPlaying)
        {
            curFootstepsSound.Stop();
            curFootstepsSound.SetLoop(false);

            curFootstepsSound = footStepSounds[(int)(type)];
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
