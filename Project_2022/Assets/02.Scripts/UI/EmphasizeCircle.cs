using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmphasizeCircle : MonoBehaviour
{
    public GameObject testTarget;
    public float sizeMin;
    public float sizeMax;
    [SerializeField]
    private Color mycol;

    private Camera mainCam;
    private GameObject targetObj;
    private bool isEmphasize;

    private void Awake()
    {
        ColorOff();
        isEmphasize = false;
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isEmphasize)
            {
                EmphasizeOff();
            }
            else
            {
                EmphasizeOn(testTarget);
            }
        }
    }

    public void EmphasizeOn(GameObject target)
    {
        targetObj = target;
        ColorOn();
        isEmphasize = true;
        StartCoroutine(Emphasizing());
    }

    public void EmphasizeOff()
    {
        isEmphasize = false;
        ColorOff();
    }

    public void ColorOn()
    {
        GetComponent<Image>().color = new Color(mycol.r, mycol.g, mycol.b, 1);
        //Debug.Log("color On");
    }
    
    public void ColorOff()
    {
        GetComponent<Image>().color = new Color(mycol.r, mycol.g, mycol.b, -1);
        //Debug.Log("color Off");
    }

    IEnumerator Emphasizing()
    {
        Vector3 targetPos = new Vector3();
        float dis = 0f;

        while (isEmphasize)
        {
            targetPos = mainCam.WorldToScreenPoint(targetObj.transform.position);
            if (targetPos.z < 0)
                ColorOff();
            else
                ColorOn();
            
            float x = Mathf.Clamp(targetPos.x, -Screen.width, Screen.width);
            float y = Mathf.Clamp(targetPos.y, -Screen.height, Screen.height);
            targetPos = new Vector3(x, y, 0);

            dis = Mathf.Lerp(sizeMin, sizeMax, 
                Vector3.Distance(mainCam.transform.position, targetObj.transform.position) / 5);

            if (Vector3.Distance(mainCam.transform.position, targetObj.transform.position) < 1)
                dis = 0;

            transform.localScale = new Vector3(dis, dis, 0);
            transform.position = targetPos;
            yield return null;
        }
    }
}
