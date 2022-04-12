using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemInfoPanelManager : MonoBehaviour
{
    [SerializeField] List<ItemInfoPanel> panels = null;

    [SerializeField] float padding = 16f;

    public static ItemInfoPanelManager Instance => _instance;
    static ItemInfoPanelManager _instance = null;

    int curOrder = 0;

    private void Awake()
    {
        _instance = this;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void Update()
    {
        SortAllPositions();
    }

    public void SetDialog(ItemInfo info)
    {
        GetDialogPanel($"{info.itemName}À»(¸¦) \nÈ¹µæÇß½À´Ï´Ù.");
    }

    private void SortAllPositions()
    {
        float height = 0;
        panels.Sort((x, y) => x.order.CompareTo(y.order));
        for (int i = 0; i < panels.Count; i++)
        {
            if(!panels[i].gameObject.activeSelf)
            {
                continue;
            }

            panels[i].rect.anchoredPosition = new Vector2(0, height);

            height += panels[i].rect.rect.height;
            height += padding;
        }
    }

    private ItemInfoPanel GetDialogPanel(string text)
    {
        ItemInfoPanel panel = null;

        if(panels.FindAll(x => x.gameObject.activeSelf).Count < 4)
        {
            panels.Sort((x, y) => x.order.CompareTo(y.order));
            panel = panels.Find(x => !x.gameObject.activeSelf);
            panel.SetText(text);
            panel.order = curOrder;
            curOrder++;
        }
        else
        {
            ItemInfoPanel[] sortedPanels = panels.ToArray();
            Array.Sort(sortedPanels, (x, y) => x.order.CompareTo(y.order));
            sortedPanels[0].RemovePanel();
        }

        panel?.gameObject.SetActive(true);

        return panel;
    }
}
