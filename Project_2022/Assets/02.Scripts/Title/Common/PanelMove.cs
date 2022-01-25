using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isOnInput = false;

    Vector3 lastMousePos = Vector3.zero;
    Vector3 lastPos = Vector3.zero;

    RectTransform parentRect = null;

    Rect camRect = Rect.zero;

    private void Start()
    {
        parentRect = gameObject.transform.parent.GetComponent<RectTransform>();
        camRect.width = Screen.width;
        camRect.height = Screen.height;
        Debug.Log(camRect);
    }

    private void FixedUpdate()
    {
        if(isOnInput)
        {
            if (lastMousePos != Vector3.zero)
            {
                Debug.Log((Vector2)(Input.mousePosition - lastMousePos));
                parentRect.anchoredPosition += (Vector2)(Input.mousePosition - lastMousePos);
                
                Vector2 clampedPosition = Vector2.zero;
                clampedPosition.x = Mathf.Clamp(parentRect.anchoredPosition.x, -camRect.width / 2 + parentRect.rect.width / 2, camRect.width / 2 - parentRect.rect.width / 2);
                clampedPosition.y = Mathf.Clamp(parentRect.anchoredPosition.y, -camRect.height / 2 + parentRect.rect.height / 2, camRect.height / 2 - parentRect.rect.height / 2);

                parentRect.anchoredPosition = clampedPosition;

                lastMousePos = Input.mousePosition;
            }
            else
            {
                lastMousePos = Input.mousePosition;
                return;
            }
            lastPos = parentRect.anchoredPosition;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!EventSystem.current.IsPointerOverGameObject(0))
        {
            isOnInput = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            isOnInput = false;
            lastMousePos = Vector3.zero;
        }
    }

}
