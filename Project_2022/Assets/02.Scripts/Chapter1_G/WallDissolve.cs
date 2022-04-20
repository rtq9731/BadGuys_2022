using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallDissolve : MonoBehaviour
{
    [SerializeField]
    private GameObject myVCam;
    [SerializeField]
    private float dissolveTime = 4;
    private MeshRenderer myMeshRnd;
    private Material myMat;

    private void Awake()
    {
        myVCam.SetActive(false);
        myMeshRnd = GetComponent<MeshRenderer>();
        myMat = myMeshRnd.material;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        WallDissolveScene();
    //    }
    //}

    public void WallDissolveScene()
    {
        
        StartCoroutine(WallAlphaDown());
    }

    IEnumerator WallAlphaDown()
    {
        yield return new WaitForSeconds(1f);

        myVCam.SetActive(true);
        UIManager._instance.OnCutScene();
        while (Vector3.Distance(Camera.main.transform.position, myVCam.transform.position) >= 0.01)
        {
            yield return null;
        }

        Color mC = myMat.GetColor("Color_C277ACA6");
        float i = 1f;

        transform.DOShakePosition(dissolveTime, new Vector3(0.05f,0.05f,0f), 3, 45, false, true);
        while (i > 0)
        {
            i -= 0.01f;
            yield return new WaitForSeconds(dissolveTime / 100);
            myMat.SetColor("Color_C277ACA6", new Color(mC.r, mC.g, mC.b, i));
        }

        UIManager._instance.OnCutSceneOver();
        gameObject.SetActive(false);
        myVCam.SetActive(false);
        Destroy(this);
    }
}
