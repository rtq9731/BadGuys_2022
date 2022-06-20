using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerColision : MonoBehaviour
{
    public GameObject curHitObj;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("StoneBtn") && hit.gameObject.GetComponent<StoneBtn>().isPressed != true)
        {
            curHitObj = hit.gameObject;
            OnTriggered(hit.gameObject);
        }
        else if (hit.transform.CompareTag("Bridge"))
        {
            curHitObj = hit.gameObject;
            OnTriggered(hit.gameObject);
        }

        //if(hit.collider.flag)
    }

    protected abstract void OnTriggeredExit(GameObject hit);
    protected abstract void OnTriggered(GameObject hit);
}
