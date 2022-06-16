using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector timeLine = null;

    [SerializeField] string loadScene;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            timeLine.Play();
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene(loadScene);
        }
    }
}
