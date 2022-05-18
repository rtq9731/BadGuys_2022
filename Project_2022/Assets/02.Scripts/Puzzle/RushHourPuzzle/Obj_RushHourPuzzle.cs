using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class RushHourCarInfo
{
    public string color;
    public ItemInfo itemInfo;
}

public class Obj_RushHourPuzzle : MonoBehaviour, IInteractAndGetItemObj
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
    public RushHourCarInfo[] carInfos;
    public ItemInfo mainIteminfo;

    // ���� �ʿ�
    [SerializeField]
    InventoryInput invenInput;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Transform playerMovePos;

    [SerializeField]
    private bool isFullCar;

    private void Awake()
    {
        Destroy(GetComponent<OutlinerOnMouseEnter>());
        Destroy(GetComponent<Outline>());
        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;
    }

    public bool CanEat()
    {
        return false;
    }

    public void Interact(ItemInfo itemInfo, GameObject taker)
    { 
        if (isFullCar) // ��� �ڵ����� á����
        {
        //    Debug.LogWarning("���̺� Ŭ����");
        //    rushHourCam.gameObject.SetActive(true);
        //    UIManager._instance.OnCutScene();
        //    rushScript.enabled = true;

        //    Debug.Log(Cursor.visible);
        //    Debug.Log(Cursor.lockState);

        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;

        //    Destroy(GetComponent<Rigidbody>());
        }
        else
        {
            if (itemInfo != null)
            {
                if (itemInfo == truckInfo) // Ʈ���� ��� �ߴٸ�
                {
                    truck.SetActive(true);
                    truck = null;

                    InventoryDisCount();
                    GetComponent<CarOutLine>().OutLineAllDisable();
                }
                else if (CheckInfo(itemInfo)) // �ڵ����� ��� ��ȣ�ۿ� �ߴٸ� 
                {
                    PutOnCarByColor(ReturnColorInInfo(itemInfo).color);
                    GetComponent<CarOutLine>().OutLineAllDisable();
                }
            }

            if (cars.Count == 0 && truck == null) // ���� �ڵ����� ��� �÷����ٸ� 
            {
                isFullCar = true;

                gameObject.AddComponent<OutlinerOnMouseEnter>();
                GetComponent<CarOutLine>().DestroySelf();

                rushHourCam.gameObject.SetActive(true);
                //UIManager.Instance.OnCutScene();
                UIManager.Instance.OnPuzzleUI();
                rushScript.enabled = true;

                Debug.Log(Cursor.visible);
                Debug.Log(Cursor.lockState);

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                Destroy(GetComponent<Rigidbody>());
            }
        }
    }

    public bool CanInteract(ItemInfo itemInfo)
    {
        if (CheckInfo(itemInfo))
        {
            mainIteminfo = itemInfo;
            return true;
        }
        else if (itemInfo == truckInfo) // Ʈ���� ��� �ߴٸ�
        {
            mainIteminfo = itemInfo;
            return true;
        }
        else if (cars.Count == 0 && truck == null)
        {
            mainIteminfo = null;
            return true;
        }
        else
        {
            mainIteminfo = null;
            return false;
        }
    }

    public void GameClear()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rushHourCam.gameObject.SetActive(false);
        rushScript.enabled = false;

        Debug.Log("Ŭ����");
        Destroy(GetComponent<OutlinerOnMouseEnter>());
        Destroy(GetComponent<Outline>());

        //UIManager.Instance.OnCutSceneOver();
        UIManager.Instance.OffPuzzleUI();
        player.transform.position = playerMovePos.position;
        player.transform.rotation = Quaternion.Euler(new Vector3(0f, 30f, 0));
        GameObject.Find("StageManager").GetComponent<StageManager>().StageChange();

        Destroy(GetComponent<Item_RushHourPuzzle>());
    }

    // ���� �´� �� ���� 
    private void PutOnCarByColor(string colorName)
    {
        GameObject target = cars.Find(item => item.GetComponent<Car>().GetColor() == colorName);

        if (target != null)
        {
            target.SetActive(true);
            cars.Remove(target);
            InventoryDisCount();
        }
    }

    private void InventoryDisCount()
    {
        invenInput.RemoveItem();
    }

    public RushHourCarInfo ReturnColorInInfo(ItemInfo info)
    {
        foreach (RushHourCarInfo carinfo in carInfos)
        {
            if (carinfo.itemInfo == info)
                return carinfo;
        }

        return new RushHourCarInfo();
    }

    public bool CheckInfo(ItemInfo info)
    {
        foreach (RushHourCarInfo carinfo in carInfos)
        {
            if (carinfo.itemInfo == info)
                return true;
        }

        return false;
    }
}
