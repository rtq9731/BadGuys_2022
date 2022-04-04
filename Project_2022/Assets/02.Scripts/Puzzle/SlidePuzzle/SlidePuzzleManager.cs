using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;
using System.Linq;
using DG.Tweening;

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
    [SerializeField]
    private SlideInterect interectManager;
    [SerializeField]
    private SlidePuzzleInput inputManager;

    public bool[] twoDVector;
    private List<Vector3> nowPos;
    private string[] fileNames;
    private string filePath = "Assets/Resources/SlidePuzzleSave";

    [SerializeField]
    private SlidePuzzlePiece lastPiece;
    [SerializeField]
    private Material lastPieceMat;

    public bool clearPuzzle;
    public System.Action clearEvent = () => { };
    public Vector2 MaxPos;
    public LayerMask target; // 그림들 레이어
    public float rayLength;
    public float pieceSpeed;

    public bool isPieceStop;

    private void Awake()
    {
        RayLengthCalul();
        isPieceStop = true;
        twoDVector = new bool[9];
        fileNames = Directory.GetFiles(filePath, "*.txt");
        pieceOriPos = new List<Vector3>();
        nowPos = new List<Vector3>();
        clearPuzzle = false;
        gridComponent.enabled = false;
        StartCoroutine(GetChildren());
        SetMaxPos(pieceOriPos[0]);
    }

    private void SetMaxPos(Vector3 firstPos)
    {
        MaxPos = firstPos;
    }

    private void RayLengthCalul()
    {
       if(rayLength == 0) rayLength = pieceWidth / panelWidth;
    }

    public void GamePause_Slide()
    {
        StartCoroutine(ChildrenEnable(false));
        inputManager.enabled = false;
        interectManager.enabled = true;
        this.enabled = false;
    }

    public void GameStart_Slide()
    {
        StartCoroutine(ChildrenEnable(true));
        inputManager.enabled = true;
    }

    public void Shuffle()
    {
        StartCoroutine(ShufflePieces());
    }

    public void ClearCheck()
    {
        StartCoroutine(CheckPiecesPos());
    }

    public List<Vector3> GetOriPoses()
    {
        return pieceOriPos.ToList();
    }

    public List<Vector3> GetNowPoses()
    {
        nowPos.Clear();
        for (int i = 0; i < pieces.Count; i++)
        {
            nowPos.Add(pieces[i].GetComponent<RectTransform>().localPosition);
        }
        return nowPos.ToList();
    }

    public void CheatClear()
    {
        lastPieceMat.DOFloat(1, "_DissolveAmount", 2f);
        clearEvent.Invoke();
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
            }
                
            
            yield return null;
        }

        clearPuzzle = clear;

        if (clear == true)
        {
            lastPieceMat.DOFloat(1, "_DissolveAmount", 2f);
            clearEvent.Invoke();
        }        
    }

    private IEnumerator ShufflePieces()
    {
        List<Vector3> loadPoses = new List<Vector3>();

        int random = Random.Range(0, fileNames.Length);

        FileInfo info = new FileInfo(fileNames[random]);
        string value = "";

        if (info.Exists)
        {
            StreamReader reader = new StreamReader(fileNames[random]);
            value = reader.ReadToEnd();
            reader.Close();
        }

        string[] textPoses = value.Split('\n');
        //Debug.Log(textPoses[0]);

        for (int i = 0; i < textPoses.Length; i++)
        {
            string[] poses = textPoses[i].Split(',');
            loadPoses.Add(new Vector3(float.Parse(poses[0]), float.Parse(poses[1]), float.Parse(poses[2])));
        }

        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].transform.GetComponent<RectTransform>().localPosition = loadPoses[i];
        }

        yield return null;
    }

    private IEnumerator GetChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<SlidePuzzlePiece>() == null)
                continue;

            GameObject child = transform.GetChild(i).gameObject;
            pieces.Add(child);
            pieceOriPos.Add(transform.GetChild(i).GetComponent<RectTransform>().localPosition);

            twoDVector[i] = true;
        }

        lastPieceMat = pieces[pieces.Count - 1].GetComponent<Image>().material;
        lastPiece = pieces[pieces.Count - 1].GetComponent<SlidePuzzlePiece>();
        lastPiece.transform.GetComponent<BoxCollider>().enabled = false;
        twoDVector[8] = false;
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
