using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerColision : MonoBehaviour
{
    public GameObject curHitObj;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if((GetComponent<CharacterController>().collisionFlags & CollisionFlags.Sides) != 0)
        {
            Debug.Log("벽과 충돌중");
            return;
        }

        curHitObj = hit.gameObject;
        OnTriggered(hit.gameObject);
    }

    protected abstract void OnTriggeredExit(GameObject hit);
    protected abstract void OnTriggered(GameObject hit);
}
