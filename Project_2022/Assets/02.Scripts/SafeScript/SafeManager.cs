using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SafeManager : MonoBehaviour
{
    public int buttonCount;
    private bool rightPassword;

    [SerializeField]
    private SafeButton[] btnOrder;
    [SerializeField]
    private List<SafeButton> btns;
    [SerializeField]
    GameObject safeDoor;

    private AudioSource audio;

    [SerializeField]
    public AudioClip wrongSound;
    [SerializeField]
    public AudioClip clearSound;

    private int wrongCount = 0;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        rightPassword = true;
        buttonCount = 0;
    }

    private void ClearSafe()
    {
        Debug.LogWarning("금고 클리어");

        FindObjectOfType<TutorialEmphasis>().idx++;
        FindObjectOfType<TutorialEmphasis>().circle.isChangeObj = true;
        audio.clip = clearSound;
        audio.Play();

        for (int i = 0; i < 5; i++)
        {
            btns[i].DestroySelf();
        }

        safeDoor.transform.DOLocalRotate(new Vector3(-90, 0, -150), 1f);
    }

    public void WrongPush()
    {
        rightPassword = true;
        buttonCount = 0;

        audio.clip = wrongSound;
        audio.Play();

        if (wrongCount >= 2)
            FindObjectOfType<GuidePanel>().OnGuide(2);

        wrongCount++;

        for (int i = 0; i < 5; i++)
        {
            btns[i].BackToNunPush();
            btns[i].canPush = true;
        }
    }

    public void Btn_Unable()
    {
        for (int i = 0; i < 5; i++)
        {
            btns[i].canPush = false;
        }
    }

    public void SafeButton_Push(string value)
    {
        buttonCount++;

        if (btnOrder[buttonCount - 1].colorName != value)
            rightPassword = false;

        if (buttonCount >= 3)
        {
            if (rightPassword)
                ClearSafe();
            else
            {
                WrongPush();
            }
                
        }
    }
}
