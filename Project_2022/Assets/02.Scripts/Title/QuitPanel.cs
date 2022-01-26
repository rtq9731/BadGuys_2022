using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitPanel : MonoBehaviour
{
    [SerializeField] Button btnQuit;
    [SerializeField] Button btnCancel;

    void Start()
    {
        btnQuit.onClick.AddListener(Application.Quit);
        btnCancel.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
