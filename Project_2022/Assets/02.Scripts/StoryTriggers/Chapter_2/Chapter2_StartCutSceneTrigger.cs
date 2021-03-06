using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter2_StartCutSceneTrigger : MonoBehaviour
{
    [SerializeField] GameObject chaseHPUI = null;
    public GameObject timeline = null;
    public RunnerAI ai = null;

    virtual public void OnStart()
    {
        UIManager.Instance.OnCutScene();
    }

    virtual public void OnComplete()
    {
        UIManager.Instance.OnCutSceneOver();
        ai.gameObject.SetActive(true);
        chaseHPUI.SetActive(true);
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
