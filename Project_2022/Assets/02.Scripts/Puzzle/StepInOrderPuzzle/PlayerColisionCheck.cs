using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisionCheck : PlayerColision
{
    protected override void OnTriggered(GameObject hit)
    {
        curHitObj.GetComponent<StoneBtn>().Pressed();
    }

    protected override void OnTriggeredExit(GameObject hit)
    {
        throw new System.NotImplementedException();
    }
}
