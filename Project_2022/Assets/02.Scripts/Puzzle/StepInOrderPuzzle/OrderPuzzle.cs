using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrderPuzzle : MonoBehaviour
{
    public GameObject[] rightOrder;
    public List<GameObject> stoneList = new List<GameObject>();

    public void CheckAnswer(GameObject stone)
    {
        stoneList.Add(stone);

        for(int i = 0; i < stoneList.Count; i++)
        {
            if(stoneList[i] != rightOrder[i])
            {
                stoneList[stoneList.Count - 1].transform.DOLocalMoveY(12.8581f, 0.1f).OnComplete(() =>
                {
                    AllReturn();
                });
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
        //정답시 나올 연출
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
