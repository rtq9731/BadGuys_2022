using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LobbyMainPicture : MonoBehaviour, IInteractableItem
{
    private bool isAllClear;

    MeshCollider mesh;

    public void Interact(GameObject taker)
    {
        LoadingTrigger.Instance.LoadingScene("Title");
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

    void Update()
    {
        
    }
}
