using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContentsSize : MonoBehaviour
{
    public static InventoryContentsSize Instance;

    private RectTransform rect;

    private void Awake()
    {
        Instance = this;
        rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        SetContentsSize();
    }

    private void Update()
    {
        SetContentsSize();
    }

    public void SetContentsSize()
    {
        float width = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            width += 140;
        }
        rect.sizeDelta = new Vector2(width, 140);
    }
}
