using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Triggers;

public class OrderPuzzle : MonoBehaviour
{
    public StoryTrigger completeTrigger = null;
    public StoryTrigger failedTrigger = null;

    public GameObject[] rightOrder;
    public List<GameObject> stoneList = new List<GameObject>();

    public AudioClip correctAnswerClip;
    public AudioClip clickSound;

    public GameObject wall = null;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CheckAnswer(GameObject stone)
    {
        stoneList.Add(stone);

        audioSource.clip = clickSound;
        audioSource.Play();

        for (int i = 0; i < stoneList.Count; i++)
        {
            if(stoneList[i] != rightOrder[i])
            {
                stoneList[stoneList.Count - 1].transform.DOLocalMoveY(12.8581f, 0.1f).OnComplete(() =>
                {
                    AllReturn();
                });
                failedTrigger.OnTriggered();
                return;
            }
            else
            {
                CorrectAnswer();
            }
        }

    }

    private void CorrectAnswer()
    {
        if(rightOrder.Length == stoneList.Count)
        {
            Debug.Log("!!!����!!!");
            completeTrigger.OnTriggered();
            for(int i = 0; i < rightOrder.Length; i++)
            {
                stoneList[i].GetComponent<BoxCollider>().enabled = false;
            }
            wall.SetActive(false);
            audioSource.clip = correctAnswerClip;
            audioSource.Play();
            return;
        }
    }

    private void AllReturn()
    {
        for(int i = 0; i < stoneList.Count; i++)
        {
            stoneList[i].GetComponent<StoneBtn>().Return();
        }

        stoneList.Clear();
    }
}
