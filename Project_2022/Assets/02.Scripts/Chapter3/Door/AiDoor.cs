using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AiDoor : MonoBehaviour
{
    public void OpenDoor()
    {
        transform.DOMoveY(50f, 1f);
    }

    public void CloseDoor()
    {
        transform.DOMoveY(180f, 1f);
    }
}
