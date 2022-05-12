using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEShooting : MonoBehaviour
{
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject shootingCam;

    public GameObject targetObj;

    QTEGenerator generator;
    RectTransform rect;

    float speed = 350f;

    bool isShooting = true;

    QTEEvents events;

    private void Start()
    {
        rect = crosshair.GetComponent<RectTransform>();
        generator = FindObjectOfType<QTEGenerator>();
        events = FindObjectOfType<QTEEvents>();
    }

    private void Update()
    {
        Shoot();
        
        if(crosshair.activeSelf)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + speed * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + -(speed * Time.unscaledDeltaTime), rect.anchoredPosition.y);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + -(speed * Time.unscaledDeltaTime));
            }
            if (Input.GetKey(KeyCode.D))
            {
                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + speed * Time.unscaledDeltaTime, rect.anchoredPosition.y);
            }
        }
    }

    void Shoot()
    {
        if(isShooting)
        {
            if (Input.GetMouseButtonDown(0) && targetObj != null)
            {
                Debug.Log("shoot");
                RectTransform targetRect = null;
                targetRect = targetObj.GetComponent<RectTransform>();
                if (targetRect.anchoredPosition.x - 80 <= rect.anchoredPosition.x &&
                    targetRect.anchoredPosition.x + 80 >= rect.anchoredPosition.x &&
                    targetRect.anchoredPosition.y - 80 <= rect.anchoredPosition.y &&
                    targetRect.anchoredPosition.y + 80 >= rect.anchoredPosition.y)
                {
                    generator.RollBtn();

                    Debug.Log("맞춤");

                    // 성공 타임라인 들어갈듯 
                }
                else
                {
                    // 실패 타임라인 들어갈듯 
                }
                EndShootingQTE();
                
                Destroy(targetObj, 0.1f);
            }
        }
    }

    public void EndShootingQTE()
    {
        UIManager.Instance.OnCutSceneOverWithoutClearDialog();
        FindObjectOfType<PlayerController>().enabled = true;
        crosshair.SetActive(false);
        shootingCam.SetActive(false);

        events.QTEKeys.RemoveAt(0);

        Time.timeScale = 1;
        isShooting = false;
    }
}