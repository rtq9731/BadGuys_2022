using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColisionCheck : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("StoneBtn") && hit.gameObject.GetComponent<StoneBtn>().isPressed != true)
        {
            OnTriggered(hit.gameObject);
        }
    }

    void OnTriggered(GameObject hit)
    {
        hit.GetComponent<StoneBtn>().Pressed();
    }
}
