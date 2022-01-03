using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyInput : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UIStackManager.IsUIStackEmpty())
            {
                UIStackManager.RemoveUIOnTop();
            }
            else
            {
                UIManager._instance.DisplayStopMenu();
            }
        }
    }
}
