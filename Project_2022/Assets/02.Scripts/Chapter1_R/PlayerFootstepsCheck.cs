using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsCheck : PlayerColision
{

    bool isOnTrigger = false;
    public void OffBridge()
    {
        SoundManager.Instance.StopLoopSound();
        SoundManager.Instance.curFootstepsSound = SoundManager.Instance.footstepsSound[0];
    }

    protected override void OnTriggered(GameObject hit)
    {
        if (hit.transform.CompareTag("Bridge") && !isOnTrigger)
        {
            SoundManager.Instance.StopLoopSound();
            SoundManager.Instance.curFootstepsSound = hit.transform.GetComponent<AudioSource>().clip;
            isOnTrigger = true;
        }
        else if (hit.transform.CompareTag("Sand") && isOnTrigger)
        {
            OffBridge();
            isOnTrigger = false;
        }
    }

    protected override void OnTriggeredExit(GameObject hit)
    {
        throw new System.NotImplementedException();
    }
}
