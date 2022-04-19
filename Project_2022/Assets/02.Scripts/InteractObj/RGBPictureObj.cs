using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RGBPictureObj : MonoBehaviour, IInteractableItem, IPlayerMouseEnterHandler, IPlayerMouseExitHandler
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

    Outline outline;

    public Material material;

    MeshRenderer mesh;

    MeshCollider collider;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        outline = GetComponent<Outline>();
        collider = GetComponent<MeshCollider>();

        outline.enabled = false;
        enabled = false;

        collider.enabled = false;

        if (sceneColor == "R")
        {
            isClearStage = true;
            enabled = true;
            collider.enabled = true;
            mesh.material = material;
        }
        if (PlayerPrefs.GetString("MainStage_StageR") == "Clear" && sceneColor == "G")
        {
            isClearStage = true;
            enabled = true;
            collider.enabled = true;
            mesh.material = material;
        }
        if (PlayerPrefs.GetString("MainStage_StageG") == "Clear" && sceneColor == "B")
        {
            isClearStage = true;
            enabled = true;
            collider.enabled = true;
            mesh.material = material;
        }

        CheckStageClear();
    }


    public void Interact(GameObject taker)
    {
        if (isClearStage && canInteract)
        {
            Debug.Log("그림 상호작용");
            pictureCam.SetActive(true);
            StartCoroutine(CameraMove());
            canInteract = false;

            outline.enabled = false;
        }
    }

    public void OnPlayerMouseEnter()
    {
        if (!enabled)
            return;

        if (isClearStage)
            outline.enabled = true;
        else
            outline.enabled = false;
    }

    public void OnPlayerMouseExit()
    {
        if (!enabled)
            return;

        outline.enabled = false;
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
            enabled = false;
            outline.enabled = false;
        }
        if (PlayerPrefs.GetString("MainStage_StageG") == "Clear" && sceneColor == "G")
        {
            enabled = false;
            outline.enabled = false;
        }
        if (PlayerPrefs.GetString("MainStage_StageB") == "Clear" && sceneColor == "B")
        {
            enabled = false;
            outline.enabled = false;
        }
    }
}
