using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseInput : MonoBehaviour
{
    [SerializeField] float playerArmLength = 1f;
    [SerializeField] LayerMask whatIsTouchable;
    PlayerController playerController;

    Transform curTouchObj = null;

    private void Update()
    {
        if (playerController._isPaused)
        {
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, playerArmLength, whatIsTouchable))
        {
            if(curTouchObj != hitInfo.transform)
            {
                curTouchObj.GetComponent<IPlayerMouseExitHandler>()?.OnPlayerMouseExit();
                curTouchObj = hitInfo.transform;
                curTouchObj.GetComponent<IPlayerMouseEnterHandler>()?.OnPlayerMouseEnter();
            }
        }
        Debug.Log(hitInfo.transform.gameObject.name);
        
        if(Input.GetMouseButtonDown(0))
        {
            curTouchObj.GetComponent<IGetPlayerMouseHandler>()?.OnGetPlayerMouse();
        }
    }
}
