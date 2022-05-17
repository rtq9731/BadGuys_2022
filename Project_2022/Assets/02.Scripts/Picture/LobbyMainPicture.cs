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

    [SerializeField] GameObject[] mainPictures;

    [SerializeField] GameObject vcamCutScene;

    [SerializeField] Outline outline = null;

    
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
        {
            outline.enabled = false;
            return false;
        }
    }

    private void StartCutScene(int pictureIdx)
    {
        UIManager.Instance.OnCutScene();
        vcamCutScene.SetActive(true);
        for (int i = 0; i < pictureIdx; i++)
        {
            mainPictures[i].gameObject.SetActive(true);
            mainPictures[i].GetComponent<SpriteRenderer>().material.SetFloat("_DissolveAmount", 1f);
        }

        mainPictures[pictureIdx].gameObject.SetActive(true);
        mainPictures[pictureIdx].GetComponent<SpriteRenderer>().material.DOFloat(1, "_DissolveAmount", 3f).OnComplete(() =>
        {
            vcamCutScene.SetActive(false);
            UIManager.Instance.OnCutSceneOver();
        });
    }

    void Start()
    {
        StartCoroutine(StartCheckCorutine());
    }

    private IEnumerator StartCheckCorutine()
    {
        yield return new WaitForSeconds(0.1f);

        if (PlayerPrefs.GetString("Chapter_1_StageB") == "Clear")
        {
            StartCutScene(2);
            isAllClear = true;
        }
        else if (PlayerPrefs.GetString("Chapter_1_StageG") == "Clear")
        {
            StartCutScene(1);
        }
        else if (PlayerPrefs.GetString("Chapter_1_StageR") == "Clear")
        {
            StartCutScene(0);
        }
    }
}
