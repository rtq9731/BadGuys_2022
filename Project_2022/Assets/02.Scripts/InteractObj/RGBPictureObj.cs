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
    bool canInteract = true;

    public Material material;

    MeshRenderer mesh;
    private void Start()
    {
        canInteract = true;
        mesh = GetComponent<MeshRenderer>();
        GetComponent<MeshCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        if (sceneColor == "R")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            mesh.material = material;
        }
        if (PlayerPrefs.GetString("MainStage_StageR") == "Clear" && sceneColor == "G")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            mesh.material = material;
        }
        if (PlayerPrefs.GetString("MainStage_StageG") == "Clear" && sceneColor == "B")
        {
            isClearStage = true;
            GetComponent<MeshCollider>().enabled = true;
            mesh.material = material;
        }

        CheckStageClear();
    }

    public void Interact(GameObject taker)
    {
        if(isClearStage && canInteract)
        {
            Debug.Log("그림 상호작용");
            pictureCam.SetActive(true);
            StartCoroutine(CameraMove());
            canInteract = false;
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

    void CheckStageClear()
    {   
        if(PlayerPrefs.GetString("MainStage_StageR") == "Clear" && sceneColor == "R")
        {
            Destroy(this);
            Destroy(GetComponent<Outline>());
        }
        if (PlayerPrefs.GetString("MainStage_StageG") == "Clear" && sceneColor == "G")
        {
            Destroy(this);
            Destroy(GetComponent<Outline>());
        }
        if (PlayerPrefs.GetString("MainStage_StageB") == "Clear" && sceneColor == "B")
        {
            Destroy(this);
            Destroy(GetComponent<Outline>());
        }
    }
}
