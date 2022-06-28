using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyInput : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.UpdateStopMenu();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !GameManager.Instance.IsPause && !UIManager.Instance.isOnCutScene)
        {
            FindObjectOfType<PlayerInteractGuide>()?.OnInput();
        }
    }
}
