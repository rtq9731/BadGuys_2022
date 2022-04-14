using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBtnManager : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject curObj;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, 1 << 9))
        {
            curObj = hit.transform.gameObject;
            curObj.transform.GetComponent<Outline>().enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                curObj.transform.GetComponent<ColorChangeBtn>().Interact();
            }
        }
        else if (curObj != null)
        {
            curObj.GetComponent<Outline>().enabled = false;
            curObj = null;
        }
    }
}
