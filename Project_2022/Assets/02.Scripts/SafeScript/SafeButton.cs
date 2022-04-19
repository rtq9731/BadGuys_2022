using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeButton : MonoBehaviour, IInteractableItem
{
    public string colorName;
    public SafeManager safeMg;
    private Vector3 firstPos;
    private AudioSource clickSound;
    public bool canPush;
    [SerializeField]
    private Material normalMat;
    [SerializeField]
    private Material pushedMat;
    private MeshRenderer myMeshRnd;

    private void Awake()
    {
        myMeshRnd = GetComponent<MeshRenderer>();
        myMeshRnd.material = normalMat;
        clickSound = GetComponent<AudioSource>();
        canPush = true;
        firstPos = transform.position;
    }

    private void Button_Push()
    {
        myMeshRnd.material = pushedMat;
        safeMg.SafeButton_Push(colorName); 
    }

    public void Interact(GameObject taker)
    {
        if (canPush)
        {
            clickSound.Play();
            transform.localPosition -= new Vector3(0, 0, 0.05f);
            canPush = false;
            //Debug.LogWarning("´­¸²");

            if (safeMg.buttonCount == 2)
            {
                myMeshRnd.material = pushedMat;
                safeMg.Btn_Unable();
                Invoke("Button_Push", 1f);
            }
            else Button_Push();
        }
    }
    public bool CanInteract()
    {
        if (!canPush || !gameObject.activeSelf || !enabled)
            return false;

        return true;
    }

    public void BackToNunPush()
    {
        transform.position = firstPos;
        myMeshRnd.material = normalMat;
        canPush = true;
        //Debug.LogWarning("µ¹¾Æ¿È");
    }

    public void DestroySelf()
    {
        Destroy(gameObject.GetComponent<OutlinerOnMouseEnter>());
        Destroy(gameObject.GetComponent<Outline>());
        Destroy(gameObject.GetComponent<SafeButton>());
    }

}
