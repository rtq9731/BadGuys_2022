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

    public void CreateRenderCam(GameObject itemObj, float originScale)
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

        itemObj.SetActive(true);

        if (itemObj.name == "Truck")
        {
            itemObj.transform.rotation = Quaternion.Euler(new Vector3(20, -150, 0));
        }
        else
        {
            itemObj.transform.rotation = Quaternion.Euler(new Vector3(-20, 44, 0));
        }

        if(itemObj.transform.GetChild(0).name.Contains("Car"))
        {
            itemObj.transform.DOScale(originScale, 0.1f);
        }
        else
        {
            obj.transform.GetChild(0).transform.localPosition = new Vector3(0.23f, 0.1f, 1f);
            itemObj.transform.DOScale(1f, 0.1f);
        }

        itemObj.transform.position = obj.transform.GetChild(0).position;
        
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
