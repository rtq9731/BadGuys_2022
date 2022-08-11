using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CreditsTitleManager : MonoBehaviour
{
    public GameObject mainCam = null;

    bool isTitle = true;
    public CanvasGroup StartCanvasGroup = null;
    public GameObject startCam = null;
    public GameObject playerCam = null;
    public GameObject timeline = null;

    // Update is called once per frame
    private void Start()
    {
        playerCam.SetActive(false);
        timeline.SetActive(false);
    }
    void Update()
    {
        if (Input.anyKeyDown && isTitle)
        {
            Debug.Log(Camera.main);
            TimeLineStart();
        }
    }

    public void GoToPlayer()
    {
        timeline.SetActive(false);
        StartCoroutine(GoToPlayerCam());
    }

    public void TimeLineStart()
    {
        isTitle = false;
        DOTween.CompleteAll();
        StartCanvasGroup.gameObject.SetActive(false);

        timeline.SetActive(true);
    }

    IEnumerator GoToPlayerCam()
    {
        playerCam.SetActive(true);
        startCam.SetActive(false);
        playerCam.GetComponent<PlayerController>().enabled = false;

        while (Vector3.Distance(mainCam.transform.position, playerCam.transform.GetChild(0).transform.position) >= 0.1f)
        {
            yield return null;
        }
        Debug.Log("asd");
        playerCam.GetComponent<PlayerController>().enabled = true;
    }
}
