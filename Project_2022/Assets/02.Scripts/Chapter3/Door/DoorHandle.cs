using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorHandle : MonoBehaviour, IInteractableItem
{
    [Header("Value")]
    public float fadeTime;

    private bool isPeek;
    private bool isMoving;

    [Header("Component")]
    public GameObject peekCam;
    public GameObject peekUI;
    public GameObject peekTxt;
    public Image fadeImg;
    public DialogDatas dialog;
    private GameObject player;
    private PlayerKeyInput keyInput;

    private bool isDialog;

    private void Awake()
    {
        isDialog = false;
        peekCam.SetActive(false);
        peekUI.SetActive(false);
        peekTxt.SetActive(false);
        fadeImg.color = new Color(0, 0, 0, 0);
        fadeImg.gameObject.SetActive(true);
        player = GameObject.Find("Player");
        keyInput = player.GetComponent<PlayerKeyInput>();
    }

    private void Update()
    {
        if (isPeek && !isMoving && !GameManager.Instance.IsPause)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isPeek = false;
                isMoving = true;
                keyInput.canScan = true;
                //UIManager.Instance.OnCutSceneOverWithoutClearDialog();
                StartCoroutine(Fading());
            }
        }
    }

    public void Interact(GameObject taker)
    {
        if (!isPeek && !isMoving)
        {
            isPeek = true;
            isMoving = true;
            keyInput.canScan = false;
            //UIManager.Instance.OnCutSceneWithoutPause();
            StartCoroutine(Fading());
            
        }
    }

    public bool CanInteract()
    {
        return true;
    }


    IEnumerator Fading()
    {
        //if (DialogManager.Instance.BoolDialog())
        //    yield return new WaitForSeconds(3f);

        if (isPeek)
        {
            UIManager.Instance.OnPuzzleUI();
        }

        bool isOver = false;
        bool outing = true;
        float value = 0;
        float a = 0;
        Color col = fadeImg.color;

        if (fadeImg.gameObject.activeSelf == false)
        {
            fadeImg.gameObject.SetActive(true);
        }

        while (isOver == false)
        {
            //Debug.Log(a);
            if (outing)
            {
                value += Time.deltaTime / fadeTime;
                a = Mathf.Clamp(value, 0, 1);
                fadeImg.color = new Color(col.r, col.g, col.b, a);

                if (a == 1)
                {
                    peekCam.SetActive(isPeek);
                    peekUI.SetActive(isPeek);
                    peekTxt.SetActive(isPeek);
                    outing = false;
                    value = 1;
                    yield return new WaitForSeconds(fadeTime);
                }
            }

            else
            {
                value -= Time.deltaTime / fadeTime;
                a = Mathf.Clamp(value, 0, 1);
                fadeImg.color = new Color(col.r, col.g, col.b, a);

                if (a == 0)
                {
                    isOver = true;
                    value = 0;
                }
            }

            
            yield return null;
        }

        if (!isDialog)
        {
            isDialog = true;
            DialogManager.Instance.SetDialogData(dialog.GetDialogs());
        }

        if (!isPeek)
        {
            UIManager.Instance.OffPuzzleUI();
        }

        isMoving = false;

        yield return null;
    }


    //----------------------------------------------------------------------------------
}
