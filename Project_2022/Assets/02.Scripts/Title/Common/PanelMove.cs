using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isOnInput = false;

    Vector3 lastMousePos = Vector3.zero;
    Vector3 mouseOffset = Vector3.zero;

    private void FixedUpdate()
    {
        if(isOnInput)
        {
            if(lastMousePos != Vector3.zero)
            {
                Debug.Log((Vector2)(Input.mousePosition - lastMousePos));
                gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition += (Vector2)(Input.mousePosition - lastMousePos);
                lastMousePos = Input.mousePosition;
            }
            else
            {
                lastMousePos = Input.mousePosition;
                return;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isOnInput = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isOnInput = false;
        lastMousePos = Vector3.zero;
    }

}
