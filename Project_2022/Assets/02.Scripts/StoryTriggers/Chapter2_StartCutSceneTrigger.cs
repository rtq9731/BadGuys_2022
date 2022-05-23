using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2_StartCutSceneTrigger : MonoBehaviour
{
    public GameObject timeline = null;
    public RunnerAI AI = null;

    virtual public void OnStart()
    {
        UIManager.Instance.OnCutScene();
    }

    virtual public void OnComplete()
    {
        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
        timeline.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.gameObject.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
