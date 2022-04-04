using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
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
    [SerializeField]
    private GameObject loadImage;
    [SerializeField]
    private Image blindImage;

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

        blindPanel.SetActive(true);

        blindImage.DOFade(1f, 1f).OnComplete(() =>
        {
            UIManager._instance.OnCutSceneOverWithoutClearDialog();
            loadImage.SetActive(true);
            StartCoroutine(LoadingSceneManager.LoadStage(sceneColor));
        });



        
    }
}
