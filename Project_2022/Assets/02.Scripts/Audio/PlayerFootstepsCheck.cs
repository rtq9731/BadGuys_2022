using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsCheck : PlayerColision
{
    [SerializeField] SoundScript[] footStepSounds;

    SoundScript curFootstepsSound;

    bool isOnTrigger = false;

    enum FloorType
    {
        Bridge,
        Sand,
        Dirt,
        Wood
    }


    public void OffBridge()
    {
        curFootstepsSound.StopLoop();
        curFootstepsSound.Stop();

        curFootstepsSound = footStepSounds[0];
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

    private void ChangefootStepSound(FloorType type)
    {
        curFootstepsSound.Stop();
        curFootstepsSound.StopLoop();

        curFootstepsSound = footStepSounds[(int)(type)];
        curFootstepsSound.SetLoop();
    }

    protected override void OnTriggeredExit(GameObject hit)
    {
        
    }
}
