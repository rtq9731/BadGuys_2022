using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchTrigger : MonoBehaviour
{
    [SerializeField] Bench eachBench = null;
    [SerializeField] Bench togetherBench = null;

    [SerializeField] GameObject nextButterflyTrigger = null;

    private void Start()
    {
        eachBench.onInteract += OnSelcectEachBench;
        togetherBench.onInteract += OnSelectTogetherBench;
    }

    private void OnSelcectEachBench()
    {
        // µ—¥Ÿ «’√∆¿ª ∂ß
        OnCompletePuzzle();
    }

    private void OnSelectTogetherBench()
    {
        // µ—¥Ÿ ∂≥æÓ∆Æ∑»¿ª ∂ß
        OnCompletePuzzle();
    }

    private void OnCompletePuzzle()
    {
        eachBench.SetActive(false);
        togetherBench.SetActive(false);
        nextButterflyTrigger.SetActive(true);
    }
}
