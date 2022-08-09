using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreditsTitleManager : MonoBehaviour
{
    public GameObject mainCam = null;

    bool isTitle = true;
    public CanvasGroup StartCanvasGroup = null;
    public GameObject playerCam = null;
    // Update is called once per frame
    private void Start()
    {
        playerCam.SetActive(false);
    }
    void Update()
    {
        if (Input.anyKeyDown && isTitle)
        {
            Debug.Log(Camera.main);
            GoToPlayer();
        }
    }

    public void GoToPlayer()
    {
        isTitle = false;
        DOTween.CompleteAll();
        StartCanvasGroup.gameObject.SetActive(false);

        StartCoroutine(GoToPlayerCam());
    }

    IEnumerator GoToPlayerCam()
    {
        playerCam.SetActive(true);
        playerCam.GetComponent<PlayerController>().enabled = false;

        while (Vector3.Distance(mainCam.transform.position, playerCam.transform.GetChild(0).transform.position) >= 0.1f)
        {
            yield return null;
        }
        Debug.Log("asd");
        playerCam.GetComponent<PlayerController>().enabled = true;
    }
}
