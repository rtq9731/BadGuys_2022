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

        for(int i = 0; i < transform.childCount; i++)
        {
            x += 5;
        }

        if (transform.childCount >= 1)
            x += transform.GetChild(transform.childCount - 1).gameObject.transform.position.x + 50;

        GameObject obj = Instantiate(renderCam, new Vector3(x ,-500,50), Quaternion.identity);

        obj.transform.SetParent(camParent);
        
        itemObj.transform.SetParent(obj.transform.GetChild(0));

        itemObj.SetActive(true);

        if (itemObj.transform.childCount > 0)
        {
            if (itemObj.transform.GetChild(0).name.Contains("car"))
            {
                itemObj.transform.DOScale(originScale, 0.1f);
                obj.transform.GetChild(0).transform.localPosition = new Vector3(0,0,1);
            }
            else if(itemObj.transform.name == "Truck")
            {
                itemObj.transform.DOScale(originScale, 0.1f);
                obj.transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 1.2f);
            }
            else
            {
                obj.transform.GetChild(0).transform.localPosition = new Vector3(0.23f, 0.1f, 1f);
                itemObj.transform.DOScale(1f, 0.1f);
            }
        }
        else
        {
            obj.transform.GetChild(0).transform.localPosition = new Vector3(0.23f, 0.1f, 1f);
            itemObj.transform.DOScale(1f, 0.1f);
        }

        if (itemObj.name == "Truck")
        {
            itemObj.transform.rotation = Quaternion.Euler(new Vector3(20, -150, 0));
        }
        else if (itemObj.name.Contains("case_button_piece"))
        {
            itemObj.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            obj.transform.GetChild(0).localPosition = new Vector3(0, 0, 0.6f);
        }
        else
        {
            itemObj.transform.rotation = Quaternion.Euler(new Vector3(-20, 44, 0));
        }

        

        if(itemObj.name.Contains("sculpture"))
        {
            ExceptionFunc(itemObj);
        }
        else
        {
            itemObj.transform.position = obj.transform.GetChild(0).position;
        }

        if(itemObj.GetComponent<Rigidbody>() != null)
        {
            itemObj.GetComponent<Rigidbody>().useGravity = false;
        }


        RenderTexture renderTexture = new RenderTexture(256,256,24, RenderTextureFormat.Default);

        obj.GetComponent<Camera>().targetTexture = renderTexture;

        SetSlotImage(obj);
    }


    public void SetSlotImage(GameObject camObj)
    {
        Slot slot = slotsParent.GetChild(slotsParent.childCount - 1).GetComponent<Slot>();

        slot.itemImage.texture = camObj.GetComponent<Camera>().targetTexture;
    }

    void ExceptionFunc(GameObject itemObj)
    {
        if (itemObj.name == "sculpture_1")
            itemObj.transform.localPosition = new Vector3(-1.45f, -0.7f, 0f);
        else if (itemObj.name == "sculpture_2")
            itemObj.transform.localPosition = new Vector3(-1.75f, -1f, -0.5f);
        else if (itemObj.name == "sculpture_3")
            itemObj.transform.localPosition = new Vector3(-2.75f, -1.35f, -0.6f);
        else if (itemObj.name == "sculpture_4")
            itemObj.transform.localPosition = new Vector3(-2.4f, -1.35f, -1.35f);

    }
}
