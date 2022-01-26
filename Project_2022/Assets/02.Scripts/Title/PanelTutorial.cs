using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTutorial : MonoBehaviour
{
    [SerializeField] Button btnTutorial = null;

    private void Start()
    {
        btnTutorial.onClick.AddListener(OnClickBtnTutorial);
    }

    public void OnClickBtnTutorial()
    {
        FindObjectOfType<CameraMoveManager>().GoToVR();
    }
}
