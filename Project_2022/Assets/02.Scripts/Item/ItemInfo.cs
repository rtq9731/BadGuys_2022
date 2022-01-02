using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item")]
public class ItemInfo : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;

    public string itemRole;

}
