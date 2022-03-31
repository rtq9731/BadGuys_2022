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
        GetComponent<MeshCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        if (sceneColor == "R")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetString("MainStage_StageR") == "Clear" && sceneColor == "G")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetString("MainStage_StageG") == "Clear" && sceneColor == "B")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Interact(GameObject taker)
    {
        if(isClearStage)
        {
            Debug.Log("�׸� ��ȣ�ۿ�");
            pictureCam.SetActive(true);
            StartCoroutine(CameraMove());
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
