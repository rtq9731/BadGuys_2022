using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LobbyMainPicture : MonoBehaviour, IInteractableItem
{
    private bool isAllClear;
    private bool isComplete = false;

    [SerializeField] Image blindImage = null;

    MeshCollider mesh;

    public void Interact(GameObject taker)
    {
        if (isComplete)
        {
            return;
        }

        isComplete = true;

        blindImage.DOFade(1, 0.5f).OnComplete(() =>
        {
            LoadingSceneManager.LoadScene("Title", true);
            GameManager.Instance.GameClear(2);
        });
    }

    void Start()
    {
        mesh = GetComponent<MeshCollider>();

        mesh.enabled = false;

        Debug.Log(PlayerPrefs.GetString("MainStage_StageB"));

        if (PlayerPrefs.GetString("MainStage_StageB") == "Clear") 
        { 
            isAllClear = true;
            mesh.enabled = true;
        }
    }
}
