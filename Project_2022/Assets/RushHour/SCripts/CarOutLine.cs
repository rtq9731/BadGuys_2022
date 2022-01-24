using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOutLine : MonoBehaviour, IPlayerMouseEnterHandler, IPlayerMouseExitHandler, IGetPlayerMouseHandler
{
    [SerializeField] OutlineMode outlineMode = OutlineMode.onlyEnterMouse;
    [SerializeField] Item_RushHourPuzzle rushHour_Item;
    [SerializeField] Transform SlotParent;
    [SerializeField] List<GameObject> carOutLines;
    [SerializeField] GameObject truckOutLine;

    private GameObject enableOutLine;

    [SerializeField]
    private bool isCheckChainge = false;

    private void Start()
    {
        for (int i = 0; i < carOutLines.Count; i++)
        {
            carOutLines[i].SetActive(false);
        }

        truckOutLine.SetActive(false);
    }

    public void OnPlayerMouseEnter()
    {
        if (outlineMode == OutlineMode.onlyEnterMouse)
        {
            if (SlotParent.childCount > 0)
            {
                SlotParent = rushHour_Item.SlotParent;
                Slot mainItemSlot;
                mainItemSlot = SlotParent.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();

                if (Inventory.Instance.MainItem == rushHour_Item.truckInfo) // 트럭을 들고 했다면
                {
                    truckOutLine.SetActive(true);
                    enableOutLine = truckOutLine;
                }

                else if (mainItemSlot.slotItem.GetComponent<CarObj>() != null) // 자동차를 들고 상호작용 했다면 
                    CarOutlineEnable(mainItemSlot.slotItem.GetComponent<CarObj>().carObjMaterial);

                isCheckChainge = true;
                StartCoroutine(CheckChainge());
            }
        }
    }

    public void OnPlayerMouseExit()
    {
        isCheckChainge = false;
        OutLineAllDisable();
    }

    public void OnGetPlayerMouse()
    {
        
    }

    void CarOutlineEnable(Material colorValue)
    {
        GameObject target = carOutLines.Find(item => item.GetComponent<CarObj>().carObjMaterial.name == colorValue.name);

        if (target != enableOutLine)
            OutLineAllDisable();

        target.SetActive(true);
        enableOutLine = target;
    }

    void OutLineAllDisable()
    {
        if (enableOutLine != null)
        {
            enableOutLine.SetActive(false);
            enableOutLine = null;
        }
    }

    public void DestroySelf()
    {
        for (int i = 0; i < carOutLines.Count; i++)
        {
            carOutLines[i].GetComponent<Outline>().enabled = false;
        }

        Destroy(GetComponent<CarOutLine>());
    }

    IEnumerator CheckChainge()
    {
        while (isCheckChainge)
        {
            
            SlotParent = rushHour_Item.SlotParent;
            Slot mainItemSlot;
            mainItemSlot = SlotParent.GetChild(Inventory.Instance.mainItemIndex).GetComponent<Slot>();

            if (Inventory.Instance.MainItem == rushHour_Item.truckInfo) // 트럭을 들고 했다면
            {
                if (enableOutLine != truckOutLine)
                    OutLineAllDisable();

                truckOutLine.SetActive(true);
                enableOutLine = truckOutLine;
            }

            else if (mainItemSlot.slotItem.GetComponent<CarObj>() != null) // 자동차를 들고 상호작용 했다면 
            {
                Material name = mainItemSlot.slotItem.GetComponent<CarObj>().carObjMaterial;
                GameObject target = carOutLines.Find(item => item.GetComponent<CarObj>().carObjMaterial.name == name.name);

                if (target != truckOutLine)
                    OutLineAllDisable();

                if (target != enableOutLine)
                    OutLineAllDisable();

                target.SetActive(true);
                enableOutLine = target;
            }

            yield return new WaitForSeconds(0f);
        }
    }
}
