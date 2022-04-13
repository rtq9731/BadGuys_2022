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

    private void Start()
    {
        eachBench.onInteract += OnSelcectEachBench;
        togetherBench.onInteract += OnSelectTogetherBench;
    }

    private void OnSelcectEachBench()
    {
        // �Ѵ� ������ ��
        attachTrigger.OnTriggered();
        OnCompletePuzzle();
    }

    private void OnSelectTogetherBench()
    {
        // �Ѵ� ����Ʈ���� ��
        eachTrigger.OnTriggered();
        OnCompletePuzzle();
    }

    private void OnCompletePuzzle()
    {
        eachBench.SetActive(false);
        togetherBench.SetActive(false);
    }
}
