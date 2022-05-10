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
    public Collider doorCol;
    public Image fadeImg;

    private void Awake()
    {
        peekCam.SetActive(false);
        peekUI.SetActive(false);
        fadeImg.color = new Color(0, 0, 0, 0);
        fadeImg.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isPeek && !isMoving)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isPeek = false;
                isMoving = true;
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
            StartCoroutine(Fading());
        }
    }

    public bool CanInteract()
    {
        return true;
    }


    IEnumerator Fading()
    {
        bool isOver = false;
        bool outing = true;
        float value = 0;
        float a = 0;
        Color col = fadeImg.color;

        if (isPeek)
        {
            UIManager.Instance.OnCutScene();
            UIManager.Instance.OnPuzzleUI();
        }

        while (isOver == false)
        {
            //Debug.Log(a);
            if (outing)
            {
                value += Time.deltaTime / fadeTime;
                a = Mathf.Clamp(value, 0, 1);
                fadeImg.color = new Color(col.r, col.g, col.b, a);
                Debug.Log(a);

                if (a == 1)
                {
                    peekCam.SetActive(isPeek);
                    peekUI.SetActive(isPeek);
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

        if (!isPeek)
        {
            UIManager.Instance.OnCutSceneOver();
            UIManager.Instance.OffPuzzleUI();
        }

        isMoving = false;

        yield return null;
    }


    //----------------------------------------------------------------------------------
}
