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

    // ���� �ʿ�
    [SerializeField]
    Transform SlotParent;
    [SerializeField]
    InventoryInput invenInput;


    private bool isFullCar;

    private void Awake()
    {
        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;
    }

    //private void PutOnCarRandom() // �ڵ��� ����
    //{
    //    int random = Random.Range(0, cars.Count);

    //    cars[random].SetActive(true);
    //    cars.Remove(cars[random]);

    //    InventoryDisCount();
    //}

    // ���� �´� �� ���� 
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

    // ������ �Լ�
    private void InventoryDisCount()
    {
        invenInput.RemoveItme();
    }
    //

    public void Interact()
    {
        if (isFullCar) // ��� �ڵ����� á����
        {
            Debug.LogWarning("���̺� Ŭ����");
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
            Slot mainItemSlot;
            if (SlotParent.childCount > 0)
            {
                mainItemSlot = SlotParent.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();
                Debug.Log(mainItemSlot.slotItem.transform.name);
            }
            else
                mainItemSlot = null;
            
            if (cars.Count == 0 && truck == null) // ���� �ڵ����� ��� �÷����ٸ� 
            {
                isFullCar = true;
                Interact();
                
            }
            else if (Inventory.Instance.MainItem == truckInfo) // Ʈ���� ��� �ߴٸ�
            {
                truck.SetActive(true);
                truck = null;

                InventoryDisCount();
            }
            else if ( mainItemSlot.slotItem.GetComponent<CarObj>() != null) // �ڵ����� ��� ��ȣ�ۿ� �ߴٸ� 
            {
                PutOnCarByColor(mainItemSlot.slotItem.GetComponent<CarObj>().carObjMaterial);
            }
            else
                return;
        }
    }

    public void GameClear()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.AddComponent<Rigidbody>();

        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;

        Debug.Log("Ŭ����");
        Destroy(GetComponent<OutlinerOnMouseEnter>());
        Destroy(GetComponent<Outline>());
        Destroy(GetComponent<Item_RushHourPuzzle>());
    }
}
