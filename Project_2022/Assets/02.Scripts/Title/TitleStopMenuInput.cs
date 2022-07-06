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
            Debug.Log("����");
            pauseMenu.gameObject.SetActive(!pauseMenu.activeSelf);
            GameManager.Instance.IsPause = pauseMenu.activeSelf;
        }
    }
}
