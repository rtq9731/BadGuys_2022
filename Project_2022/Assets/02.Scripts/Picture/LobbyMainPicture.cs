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

    [SerializeField]
    GameObject[] mainPictures;
    [SerializeField]
    GameObject mainPicture;

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

    public bool CanInteract()
    {
        if (!enabled || !gameObject.activeSelf)
            return false;

        if (isAllClear)
            return true;
        else
            return false;
    }

    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("MainStage_StageB"));

        if (PlayerPrefs.GetString("MainStage_StageR") == "Clear")
        {
            mainPictures[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetString("MainStage_StageG") == "Clear")
        {
            mainPictures[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetString("MainStage_StageB") == "Clear") 
        {
            mainPicture.SetActive(true);
            isAllClear = true;
        }
    }
}
