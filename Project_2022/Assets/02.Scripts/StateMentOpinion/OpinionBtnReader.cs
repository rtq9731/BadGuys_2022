using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpinionBtnReader : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> opinionBtnManagers;

    public void ButtonLoad()
    {
        Debug.LogWarning("버튼 로드 실행");
        Instantiate(opinionBtnManagers[GameManager.Instance.stateNum], transform);
    }
}
