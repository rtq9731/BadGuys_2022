using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AiDoor : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        anim.SetTrigger("OpenTri");
    }

    public void CloseDoor()
    {
        anim.SetTrigger("CloseTri");
    }
}
