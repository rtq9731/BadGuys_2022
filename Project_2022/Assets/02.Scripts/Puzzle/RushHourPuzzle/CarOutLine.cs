using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOutLine : MonoBehaviour, IPlayerMouseEnterHandler, IPlayerMouseExitHandler, IGetPlayerMouseHandler
{
    [SerializeField] OutlineMode outlineMode = OutlineMode.onlyEnterMouse;
    [SerializeField] Obj_RushHourPuzzle rushHour_Obj;
    [SerializeField] Inventory inventory;
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
        ItemInfo mainItem = null;
        if (inventory.MainItem != null)
            mainItem = inventory.MainItem;

        if (outlineMode == OutlineMode.onlyEnterMouse)
        {
            if (mainItem != null)
            {
                if (mainItem == rushHour_Obj.truckInfo) // 트럭을 들고 했다면
                {
                    truckOutLine.SetActive(true);
                    enableOutLine = truckOutLine;
                }

                else if (rushHour_Obj.CheckInfo(mainItem)) // 자동차를 들고 상호작용 했다면 
                    CarOutlineEnable(rushHour_Obj.ReturnColorInInfo(mainItem).color);

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

    void CarOutlineEnable(string colorName)
    {
        GameObject target = carOutLines.Find(item => item.GetComponent<CarObj>().carObjMaterial.name == colorName);

        if (target != enableOutLine)
            OutLineAllDisable();

        target.SetActive(true);
        enableOutLine = target;
    }

    public void OutLineAllDisable()
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
            truckOutLine.GetComponent<Outline>().enabled = false;
        }
        OutLineAllDisable();
        Destroy(GetComponent<CarOutLine>());
    }

    IEnumerator CheckChainge()
    {
        while (isCheckChainge)
        {
            ItemInfo mainItem = null;
            if (inventory.MainItem != null)
                mainItem = inventory.MainItem;

            if (mainItem == rushHour_Obj.truckInfo) // 트럭을 들고 했다면
            {
                if (enableOutLine != truckOutLine)
                    OutLineAllDisable();

                truckOutLine.SetActive(true);
                enableOutLine = truckOutLine;
            }

            else if (rushHour_Obj.CheckInfo(mainItem)) // 자동차를 들고 상호작용 했다면 
            {
                string name = rushHour_Obj.ReturnColorInInfo(mainItem).color;
                GameObject target = carOutLines.Find(item => item.GetComponent<CarObj>().carObjMaterial.name == name);

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
