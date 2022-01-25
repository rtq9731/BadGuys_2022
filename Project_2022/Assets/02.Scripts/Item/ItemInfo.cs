using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "item", menuName = "ScriptableObject/Item")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public RawImage itemImage;

    public string itemRole;
}
