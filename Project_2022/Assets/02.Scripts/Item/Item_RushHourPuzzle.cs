using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Item_RushHourPuzzle : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    CinemachineVirtualCamera rushHourCam;
    [SerializeField]
    RushHourManger rushScript;

    private void Awake()
    {
        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;
    }

    public void Interact()
    {
        Debug.LogWarning("테이블 클릭됨");
        rushHourCam.gameObject.SetActive(true);
        rushScript.enabled = true;

        Debug.Log(Cursor.visible);
        Debug.Log(Cursor.lockState);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Destroy(GetComponent<Rigidbody>());
    }

    public void GameClear()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.AddComponent<Rigidbody>();

        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;

        Debug.Log("클리어");
        Destroy(GetComponent<OutlinerOnMouseEnter>());
        Destroy(GetComponent<Outline>());
        Destroy(GetComponent<Item_RushHourPuzzle>());
    }
}
