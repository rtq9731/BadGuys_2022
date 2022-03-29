using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlidePuzzlePiece : MonoBehaviour
{
    private SlidePuzzleManager SPManager;
    private RectTransform rectTransform;
    private float speed;
    private List<Vector3> poses;
    private int myPointNum;
    private Vector3 myPoint;

    private void Awake()
    {
        myPoint = new Vector3();
        rectTransform = GetComponent<RectTransform>();
        SPManager = transform.GetComponentInParent<SlidePuzzleManager>();
        speed = SPManager.pieceSpeed;
    }
    private void Start()
    {
        poses = SPManager.GetOriPoses();
    }

    private void Update()
    {
        //rayLength = SPManager.rayLength;
        //Debug.DrawRay(transform.position, Vector3.left * rayLength, Color.red);
    }

    private bool CalculNearByEmpty(int empty)
    {
        if (empty - 3 == myPointNum)
            return true;
        if (empty + 3 == myPointNum)
            return true;
        if (empty + 1 == myPointNum)
            return true;
        if (empty - 1 == myPointNum)
            return true;

        return false;
    }

    public void MoveToDis()
    {
        StartCoroutine(FindMyPoint());
        StartCoroutine(FindNextPoint());
    }

    public void Selected(Color color)
    {
        GetComponent<Image>().color = color;
        
    }

    public void UnSelected()
    {
        GetComponent<Image>().color = new Color(255, 255, 255, 255); // Èò»ö
    }

    private IEnumerator FindMyPoint()
    {
        for (int i = 0; i < poses.Count; i++)
        {
            if (rectTransform.localPosition == poses[i])
            {
                myPoint = poses[i];
                myPointNum = i;
                break;
            }
        }
        yield return null;
    }

    private IEnumerator FindNextPoint()
    {
        int emptySpace = 0;

        for (int i = 0; i < SPManager.twoDVector.Length; i++)
        {
            if (SPManager.twoDVector[i] == false)
            {
                emptySpace = i;
            }
        }

        if (CalculNearByEmpty(emptySpace))
        {
            SPManager.isPieceStop = false;
            SPManager.twoDVector[myPointNum] = false;
            rectTransform.DOLocalMove(poses[emptySpace], speed);
            yield return new WaitForSeconds(speed);
            SPManager.twoDVector[emptySpace] = true;
            myPointNum = emptySpace;
            myPoint = poses[emptySpace];
        }
        else
        {
            rectTransform.DOShakePosition(speed, new Vector3(3, 3, 0));
            yield return new WaitForSeconds(speed);
        }

        SPManager.ClearCheck();
        SPManager.isPieceStop = true;
        yield return null;
    }
}
