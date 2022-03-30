using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class RGBPictureObj : MonoBehaviour, IInteractableItem
{
    [SerializeField]
    private string sceneColor;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private GameObject pictureCam;
    [SerializeField]
    private GameObject blindPanel;

    bool isClearStage = false;

    private void Start()
    {

        Debug.Log(PlayerPrefs.GetString("RStage"));
        Debug.Log(PlayerPrefs.GetString("GStage"));
        Debug.Log(PlayerPrefs.GetString("BStage"));
        GetComponent<MeshCollider>().enabled = false;
        if (sceneColor == "R")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            return;
        }
        else if (PlayerPrefs.GetString("RStage") != "Clear" && sceneColor == "G")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            return;
        }
        else if (PlayerPrefs.GetString("GStage") != "Clear" && sceneColor == "B")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            return;
        }
    }

    public void Interact(GameObject taker)
    {
        if(isClearStage)
        {
            Debug.Log("그림 상호작용");
            pictureCam.SetActive(true);
            StartCoroutine(CameraMove());
            //SceneManager.LoadScene("MainStage_StageR");
            MeshCollider mesh = transform.GetComponent<MeshCollider>();
            mesh.enabled = false;
        }
    }

    IEnumerator CameraMove()
    {
        UIManager._instance.OnCutScene();
        while (Vector3.Distance(pictureCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(0.4f);

        pictureCam.transform.DOLocalMoveZ(0.2f, 1f).OnComplete(() =>
        {
            Debug.Log("asdasd");
            blindPanel.SetActive(true);
            UIManager._instance.OnCutSceneOverWithoutClearDialog();
            StartCoroutine(LoadingSceneManager.LoadStage(sceneColor));
        });
    }
}
