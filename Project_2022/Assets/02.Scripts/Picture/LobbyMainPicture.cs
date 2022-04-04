using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMainPicture : MonoBehaviour, IInteractableItem
{
    private bool isAllClear;

    MeshCollider mesh;

    public void Interact(GameObject taker)
    {
        if(isAllClear)
        {

        }
    }

    void Start()
    {
        mesh = GetComponent<MeshCollider>();

        mesh.enabled = false;

        if (PlayerPrefs.GetString("MainStage_StageR") == "Clear") { isAllClear = false; }
        if (PlayerPrefs.GetString("MainStage_StageG") == "Clear") { isAllClear = false; }
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
