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
    [SerializeField]
    List<GameObject> cars;
    [SerializeField]
    GameObject truck;
    [SerializeField]
    ItemInfo carInfo;
    [SerializeField]
    ItemInfo truckInfo;

    // 질문 필요
    [SerializeField]
    Transform SlotParent;
    

    private bool isFullCar;

    private void Awake()
    {
        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;
    }

    private void PutOnCarRandom() // 자동차 생성
    {
        int random = Random.Range(0, cars.Count);

        cars[random].SetActive(true);
        cars.Remove(cars[random]);

        InventoryDisCount();
    }

    // 색에 맞는 차 생성 
    //private void PutOnCarByColor(string colorValue)
    //{
    //    GameObject target = cars.Find(item => item.GetComponent<Car>().GetColor() == colorValue);

    //    if (target == null)
    //    {

    //    }
    //    else
    //    {
    //        target.SetActive(true);
    //        cars.Remove(target);
    //    }
    //}

    // 질문할 함수
    private void InventoryDisCount()
    {
        Slot mainSlot = SlotParent.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();
        Debug.LogWarning(mainSlot.GetItemCount());
        if (mainSlot.GetItemCount() > 0)
        {
            mainSlot.DiscountItemCount(1);
        }
        else
        {
            mainSlot.DestroyItemSlot();
        }
    }
    //

    public void Interact()
    {
        if (isFullCar) // 모든 자동차가 찼으면
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
        else
        {
            if (cars.Count == 0 && truck == null) // 자동차를 들고 상호작용 했다면
            {
                isFullCar = true;
                Interact();
                
            }
            else if (Inventory.Instance.MainItem == truckInfo) // 트럭을 들고 했다면
            {
                truck.SetActive(true);
                truck = null;

                InventoryDisCount();
            }
            else if (Inventory.Instance.MainItem == carInfo) // 만약 자동차가 모두 올려졌다면 
            {
                PutOnCarRandom();
            }
        }
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
