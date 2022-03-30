using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Triggers;

public class BenchTrigger : MonoBehaviour
{
    [SerializeField] Bench eachBench = null;
    [SerializeField] Bench togetherBench = null;

    [SerializeField] StoryTrigger eachTrigger = null;
    [SerializeField] StoryTrigger attachTrigger = null;

    [SerializeField] GameObject nextButterflyTrigger = null;
    [SerializeField] GameObject wall = null;

    private void Start()
    {
        eachBench.onInteract += OnSelcectEachBench;
        togetherBench.onInteract += OnSelectTogetherBench;
    }

    private void OnSelcectEachBench()
    {
        // µ—¥Ÿ «’√∆¿ª ∂ß
        attachTrigger.OnTriggered();
        OnCompletePuzzle();
    }

    private void OnSelectTogetherBench()
    {
        // µ—¥Ÿ ∂≥æÓ∆Æ∑»¿ª ∂ß
        eachTrigger.OnTriggered();
        OnCompletePuzzle();
    }

    private void OnCompletePuzzle()
    {
        wall.SetActive(false);
        eachBench.SetActive(false);
        togetherBench.SetActive(false);
        nextButterflyTrigger.SetActive(true);
    }
}
