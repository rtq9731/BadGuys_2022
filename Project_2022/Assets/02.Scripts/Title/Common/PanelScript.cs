using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    [SerializeField] RectTransform topRect;
    [SerializeField] Button btnExit;

    private void Start()
    {
        btnExit.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
