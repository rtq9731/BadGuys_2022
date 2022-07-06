using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStopMenuInput : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.IsPause = true;
            pauseMenu.gameObject.SetActive(!pauseMenu.activeSelf);
        }
    }
}
