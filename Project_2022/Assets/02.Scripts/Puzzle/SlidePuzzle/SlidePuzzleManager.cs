using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using DG.Tweening;

public class SlidePuzzleManager : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup gridComponent;
    [SerializeField]
    private SlideInterect slideInterect;
    [SerializeField]
    private List<GameObject> pieces;
    [SerializeField]
    private List<Vector3> pieceOriPos;
    [SerializeField]
    private float pieceWidth;
    [SerializeField]
    private float panelWidth;
    [SerializeField]
    private SlideInterect interectManager;
    [SerializeField]
    private SlidePuzzleInput inputManager;

    private List<Vector3> pieceShufflePos;

    [SerializeField]
    private SlidePuzzlePiece lastPiece;
    [SerializeField]
    private Material lastPieceMat;

    public bool clearPuzzle;
    public UnityEvent clearEvent;
    public Vector2 MaxPos;
    public LayerMask target; // 그림들 레이어
    public float rayLength;
    public float pieceSpeed;

    private void Awake()
    {
        RayLengthCalul();
        pieceOriPos = new List<Vector3>();
        pieceShufflePos = new List<Vector3>();
        clearPuzzle = false;
        gridComponent.enabled = false;
        StartCoroutine(GetChildren());
        SetMaxPos(pieceOriPos[0]);
    }

    private void SetMaxPos(Vector3 firstPos)
    {
        Debug.Log(firstPos);
        MaxPos = firstPos;
        Debug.Log(MaxPos);
    }

    private void RayLengthCalul()
    {
       if(rayLength == 0) rayLength = pieceWidth / panelWidth;
    }

    public void GamePause_Slide()
    {
        StartCoroutine(ChildrenEnable(false));
        interectManager.enabled = true;
        this.enabled = false;
    }

    public void GameStart_Slide()
    {
        StartCoroutine(ChildrenEnable(true));
    }

    public void Shuffle()
    {
        StartCoroutine(ShufflePieces());
    }

    public void ClearCheck()
    {
        StartCoroutine(CheckPiecesPos());
    }

    public List<Vector3> GetPoses()
    {
        return pieceOriPos.ToList();
    }

    private IEnumerator CheckPiecesPos()
    {
        bool clear = true;

        for (int i = 0; i < pieceOriPos.Count - 1; i++)
        {
            Vector3 oriPos = pieceOriPos[i];
            if (pieces[i].transform.GetComponent<RectTransform>().localPosition != oriPos)
            {
                clear = false;
                //Debug.Log(i + " 번째 퍼즐 안맞음");
            }
                
            
            yield return null;
        }

        clearPuzzle = clear;

        if (clear == true)
        {
            Debug.Log("Clear");
            lastPiece.UnSetTouchEnable(true);
            lastPieceMat.DOFloat(1, "_DissolveAmount", 2f);
            clearEvent.Invoke();
        }        
    }

    private IEnumerator ShufflePieces()
    {
        Debug.Log("셔플 시작");
        pieceShufflePos = pieceOriPos.ToList();

        int count = pieceShufflePos.Count;
        for (int i = 0; i < count; i++)
        {
            int ran = Random.Range(0, pieceShufflePos.Count);
            pieces[i].transform.localPosition = pieceShufflePos[ran];
            pieceShufflePos.RemoveAt(ran);
        }

        Debug.Log("셔플끝");
        yield return null;
    }

    private IEnumerator GetChildren()
    {
        Debug.Log(transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<SlidePuzzlePiece>() == null)
                continue;

            GameObject child = transform.GetChild(i).gameObject;
            pieces.Add(child);
            pieceOriPos.Add(transform.GetChild(i).GetComponent<RectTransform>().localPosition);
        }

        lastPieceMat = pieces[pieces.Count - 1].GetComponent<Image>().material;
        lastPiece = pieces[pieces.Count - 1].GetComponent<SlidePuzzlePiece>();
        lastPiece.UnSetTouchEnable(false);
        lastPieceMat.SetFloat("_DissolveAmount", 0f);
        yield return null;
    }

    private IEnumerator ChildrenEnable(bool enable)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            SlidePuzzlePiece childPiece = transform.GetChild(i).GetComponent<SlidePuzzlePiece>();

            if (childPiece == null)
                continue;

            childPiece.enabled = enable;

            yield return null;
        }
    }
}
