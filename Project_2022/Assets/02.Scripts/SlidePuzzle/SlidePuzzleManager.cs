using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class SlidePuzzleManager : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup gridComponent;
    [SerializeField]
    private List<GameObject> pieces;
    [SerializeField]
    private List<Vector3> pieceOriPos;
    [SerializeField]
    private float pieceWidth;
    [SerializeField]
    private float panelWidth;

    private List<Vector3> pieceShufflePos;

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
        MaxPos = new Vector2(Mathf.Abs(firstPos.x), Mathf.Abs(firstPos.y));
    }

    private void RayLengthCalul()
    {
        rayLength = pieceWidth / panelWidth;
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
            if (pieces[i].transform.position - oriPos != null)
            {
                clear = false;
                Debug.Log(i + " 번째 퍼즐 안맞음");
            }
                
            
            yield return null;
        }

        clearPuzzle = clear;

        if (clear == true)
        {
            Debug.Log("Clear");
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
            if (transform.GetChild(i).gameObject.activeSelf == false)
                continue;

            GameObject child = transform.GetChild(i).gameObject;
            pieces.Add(child);
            pieceOriPos.Add(transform.GetChild(i).GetComponent<RectTransform>().localPosition);
        }

        pieces[pieces.Count - 1].SetActive(false);
        yield return null;
    }
}
