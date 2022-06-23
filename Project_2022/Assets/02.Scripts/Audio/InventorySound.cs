using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySound : MonoBehaviour
{
    public SoundScript getItemSound;

    public void GetItemSoundPlay()
    {
        getItemSound.Play();
    }
}
