using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System.Linq;

public class PiecePuzzleManager : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> pieceOriPos;
    [SerializeField]
    private List<GameObject> pieceGameObj;

    public UnityEvent clear;
    public UnityEvent pieceIn;
    public float durationTime;
    public float appearTime;
    public float clearDistance;
    public int targetPieceNum;
    public GameObject targetPiece;
    public bool pieceCanMove;

    private PiecePuzzleInput inputSystem;
    private bool cheat;

    private void Awake()
    {
        cheat = false;
        inputSystem = GetComponentInParent<PiecePuzzleInput>();
        pieceCanMove = false;
        StartCoroutine(SetChildrenInList());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (Input.GetKeyDown(KeyCode.K) && !cheat)
            {
                cheat = true;
                CheatClear();
            }
        }
    }

    private List<GameObject> ListShuffle(List<GameObject> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            int rnd = Random.Range(0, i);

            GameObject temp = _list[i];
            _list[i] = _list[rnd];
            _list[rnd] = temp;
        }

        return _list;
    }

    public void PuzzleSetting()
    {
        targetPieceNum = 0;
        targetPieceSetting();
        inputSystem.ChaingeTarget();
    }

    public Vector3 FindPiecePos(GameObject pieceObj)
    {
        int num = 0;

        for (int i = 0; i < pieceGameObj.Count; i++)
        {
            if (pieceGameObj[i] == pieceObj)
            {
                num = i;
                break;
            }
        }

        return pieceOriPos[num];
    }

    public void PutPieceIn()
    {
        if (targetPieceNum + 1 < pieceGameObj.Count)
        {
            targetPieceNum++;
            targetPieceSetting();
            pieceIn.Invoke();
        }
        else
            ClearPuzzle();
    }

    private void ClearPuzzle()
    {
        Debug.LogWarning("Clear");
        clear.Invoke();
        PiecePuzzleAllClear.Instance.AddClearCount();
        this.enabled = false;
    }

    private void targetPieceSetting()
    {
        pieceCanMove = false;
        targetPiece = pieceGameObj[targetPieceNum];
        targetPiece.SetActive(true);
        targetPiece.transform.localPosition = Vector3.zero;
        targetPiece.GetComponent<PiecePuzzlePiece>().AppearPiece();

    }

    private void CheatClear()
    {
        Debug.Log("�װ� �� �Ǹ��ε�");
        inputSystem.enabled = false;
        targetPiece.transform.DOLocalMove(FindPiecePos(targetPiece), 0.5f);

        for (int i = 0; i < pieceGameObj.Count; i++)
        {
            pieceGameObj[i].SetActive(true);
            pieceGameObj[i].GetComponent<PiecePuzzlePiece>().AppearPiece();
        }
        Invoke("ClearPuzzle", 1.5f);
    }

    private IEnumerator SetChildrenInList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject piece = transform.GetChild(i).gameObject;

            pieceGameObj.Add(piece);
            piece.SetActive(false);
        }

        pieceGameObj = ListShuffle(pieceGameObj);

        for (int i = 0; i < pieceGameObj.Count; i++)
        {
            GameObject piece = pieceGameObj[i];
            pieceOriPos.Add(piece.transform.localPosition);
        }

        yield return null;
    }
}
