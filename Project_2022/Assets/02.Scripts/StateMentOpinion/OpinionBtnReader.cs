using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpinionBtnReader : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> opinionBtnManagers;

    public void ButtonLoad()
    {
        Debug.LogWarning("��ư �ε� ����");
        Instantiate(opinionBtnManagers[GameManager.Instance.stateNum], transform);
    }
}
