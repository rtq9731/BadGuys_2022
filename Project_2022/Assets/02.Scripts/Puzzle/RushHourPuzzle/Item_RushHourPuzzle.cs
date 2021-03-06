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
    public ItemInfo truckInfo;

    // 질문 필요
    public Transform SlotParent;
    [SerializeField]
    InventoryInput invenInput;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform playerMovePos;


    private bool isFullCar;

    private void Awake()
    {
        Destroy(GetComponent<OutlinerOnMouseEnter>());
        Destroy(GetComponent<Outline>());
        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;
    }

    //private void PutOnCarRandom() // 자동차 생성
    //{
    //    int random = Random.Range(0, cars.Count);

    //    cars[random].SetActive(true);
    //    cars.Remove(cars[random]);

    //    InventoryDisCount();
    //}

    // 색에 맞는 차 생성 
    private void PutOnCarByColor(Material colorValue)
    {
        GameObject target = cars.Find(item => item.GetComponent<Car>().GetColor() == colorValue.name);

        if (target != null)
        {
            target.SetActive(true);
            cars.Remove(target);
            InventoryDisCount();
        }
    }

    // 질문할 함수
    private void InventoryDisCount()
    {
        invenInput.RemoveItem();
    }
    //

    public bool CanEat()
    {
        return false;
    }

    public void Interact(GameObject taker)
    {
        if (isFullCar) // 모든 자동차가 찼으면
        {
            Debug.LogWarning("테이블 클릭됨");
            rushHourCam.gameObject.SetActive(true);
            UIManager.Instance.OnCutScene();
            rushScript.enabled = true;

            Debug.Log(Cursor.visible);
            Debug.Log(Cursor.lockState);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Destroy(GetComponent<Rigidbody>());
        }
        else
        {
            Slot mainItemSlot = null;
            if (SlotParent.childCount > 0)
            {
                mainItemSlot = SlotParent.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();

                if (Inventory.Instance.MainItem == truckInfo) // 트럭을 들고 했다면
                {
                    truck.SetActive(true);
                    truck = null;

                    InventoryDisCount();
                    GetComponent<CarOutLine>().OutLineAllDisable();
                }
                else if (mainItemSlot.slotItem.GetComponent<CarObj>() != null) // 자동차를 들고 상호작용 했다면 
                {
                    PutOnCarByColor(mainItemSlot.slotItem.GetComponent<CarObj>().carObjMaterial);

                    GetComponent<CarOutLine>().OutLineAllDisable();
                }
            }

            if (cars.Count == 0 && truck == null) // 만약 자동차가 모두 올려졌다면 
            {
                isFullCar = true;

                gameObject.AddComponent<OutlinerOnMouseEnter>();
                GetComponent<CarOutLine>().DestroySelf();
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

        UIManager.Instance.OnCutSceneOver();
        GameObject.Find("StageManager").GetComponent<StageManager>().StageChange();

        player.transform.position = playerMovePos.position;
        Destroy(GetComponent<Item_RushHourPuzzle>());
    }

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        return true;
    }
}
