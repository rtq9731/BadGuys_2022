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
        // �Ѵ� ������ ��
        OnCompletePuzzle();
    }

    private void OnSelectTogetherBench()
    {
        // �Ѵ� ����Ʈ���� ��
        OnCompletePuzzle();
    }

    private void OnCompletePuzzle()
    {
        eachBench.SetActive(false);
        togetherBench.SetActive(false);
        nextButterflyTrigger.SetActive(true);
    }
}
