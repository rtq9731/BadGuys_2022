using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadKey : MonoBehaviour, IInteractableItem
{
    private Keypad keypad;
    
    public KeyType keyType = KeyType.clear;
    public int value = 0;

    private void Awake()
    {
        keypad = transform.GetComponentInParent<Keypad>();
    }

    public void Interact(GameObject taker)
    {
        keypad.KeyInput(keyType, "" + value);
    }

    public bool CanInteract()
    {
        return true;
    }
}
