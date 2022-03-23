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
    public void Interact(GameObject taker)
    {
        Debug.Log("그림 상호작용");
        pictureCam.SetActive(true);
        StartCoroutine(CameraMove());

        MeshCollider mesh = transform.GetComponent<MeshCollider>();
        mesh.enabled = false;
    }

    IEnumerator CameraMove()
    {
        while (Vector3.Distance(pictureCam.transform.position, mainCam.transform.position) >= 0.1f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.4f);

        pictureCam.transform.DOLocalMoveZ(0.2f, 1f).OnComplete(() =>
        {
            Debug.Log("asdasd");
            blindPanel.SetActive(true);
            StartCoroutine(LoadingSceneManager.LoadStage(sceneColor));
            
        });
    }
}
