using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEShooting : MonoBehaviour
{
    [SerializeField] GameObject crosshair;

    public GameObject targetObj;

    QTEGenerator generator;
    RectTransform rect;

    float speed = 200;

    float shootDelay = 1f;

    private void Start()
    {
        rect = crosshair.GetComponent<RectTransform>();
        generator = FindObjectOfType<QTEGenerator>();
    }

    private void Update()
    {
        shootDelay += Time.unscaledDeltaTime;

        Shoot();

        if (Input.GetKey(KeyCode.W))
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + speed * Time.unscaledDeltaTime);
            Debug.Log("W");
        }
        if(Input.GetKey(KeyCode.A))
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + -(speed * Time.unscaledDeltaTime), rect.anchoredPosition.y);
            Debug.Log("A");
        }
        if(Input.GetKey(KeyCode.S))
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + -(speed * Time.unscaledDeltaTime));
            Debug.Log("S");
        }
        if(Input.GetKey(KeyCode.D))
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + speed * Time.unscaledDeltaTime, rect.anchoredPosition.y);
            Debug.Log("D");
        }
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            shootDelay = 0;
            Debug.Log("shoot");
            RectTransform targetRect = null;
            targetRect = targetObj.GetComponent<RectTransform>();
            if(targetRect.anchoredPosition.x - 80  <= rect.anchoredPosition.x &&
                targetRect.anchoredPosition.x + 80 >= rect.anchoredPosition.x &&
                targetRect.anchoredPosition.y - 80 <= rect.anchoredPosition.y &&
                targetRect.anchoredPosition.y + 80 >= rect.anchoredPosition.y)
            {

                generator.RollBtn();
                Destroy(targetObj, 0.1f);

                generator.Generation();
                Debug.Log("¸ÂÃã");
            }

        }
    }

   
}