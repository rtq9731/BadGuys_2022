using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreateRenderTextureCam : MonoBehaviour
{
    [SerializeField]
    private Transform camParent;
    [SerializeField]
    private Transform slotsParent;

    public GameObject renderCam;



    public void CreateRenderCam(GameObject itemObj)
    {
        float x = 0;

        Debug.Log(transform.childCount);

        for(int i = 0; i < transform.childCount; i++)
        {
            x += 5;
        }

        GameObject obj = Instantiate(renderCam, new Vector3(50+x,50,50), Quaternion.identity);

        obj.transform.SetParent(camParent);
        
        itemObj.transform.SetParent(obj.transform.GetChild(0));
        
        itemObj.transform.position = obj.transform.GetChild(0).position;
        itemObj.transform.DOScale(0.1f, 0.1f);

        if(itemObj.name == "Truck")
        {
            itemObj.transform.rotation = Quaternion.Euler(new Vector3(20, -150, 0));
        }
        else
        {
            itemObj.transform.rotation = Quaternion.Euler(new Vector3(-20, 44, 0));
        }

        itemObj.SetActive(true);

        RenderTexture renderTexture = new RenderTexture(256,256,24, RenderTextureFormat.Default);

        


        obj.GetComponent<Camera>().targetTexture = renderTexture;

        SetSlotImage(obj);
    }


    public void SetSlotImage(GameObject camObj)
    {
        Slot slot = slotsParent.GetChild(slotsParent.childCount - 1).GetComponent<Slot>();

        slot.itemImage.texture = camObj.GetComponent<Camera>().targetTexture;
    }
}
