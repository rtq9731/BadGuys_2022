 using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PatrolCheck : MonoBehaviour
{
    public static PatrolCheck Instanse;

    public bool isPlayerIn;
    public bool isDoorClose;
    public StageRestart restart;

    private void Awake()
    {
        if (Instanse == null)
            Instanse = this;
        else if (Instanse != this)
            Destroy(this.gameObject);
        SceneManager.sceneLoaded += LoadedsceneEvent;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerIn = true;
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerIn = false;
    }

    public bool IsHide()
    {
        return (isPlayerIn && isDoorClose);
    }

    public void EndGame()
    {
        restart.Detection("TIP : 조직원들에게 발각되지 않도록 조심하세요.");
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        isPlayerIn = true;
        isDoorClose = true;
    }
}
