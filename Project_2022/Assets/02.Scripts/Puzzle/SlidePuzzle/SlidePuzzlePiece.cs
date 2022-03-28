using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlidePuzzlePiece : MonoBehaviour
{
    private SlidePuzzleManager SPManager;
    private float rayLength;
    private LayerMask target;
    private RectTransform rectTransform;
    private float speed;
    private List<Vector3> poses;

    Vector2 maximumPos;

    Material myMT;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        SPManager = transform.GetComponentInParent<SlidePuzzleManager>();
        target = SPManager.target;
        speed = SPManager.pieceSpeed;
    }
    private void Start()
    {
        maximumPos = SPManager.MaxPos;
        rayLength = SPManager.rayLength;
        poses = SPManager.GetPoses();
    }

    private void Update()
    {
        //rayLength = SPManager.rayLength;
        //Debug.DrawRay(transform.position, Vector3.left * rayLength, Color.red);
    }

    public void UnSetTouchEnable(bool value)
    {
        GetComponent<BoxCollider>().enabled = value;
    }

    public void MoveToPos(Vector3 pos)
    {
        
        Direction dir = CalculDir(pos);

        //Debug.Log(dir);
        //Debug.Log(pos);

        Vector3 AddPos = Vector3.zero;
        if (dir == Direction.Left)
            AddPos += new Vector3(-speed, 0, 0);
        else if (dir == Direction.Right)
            AddPos += new Vector3(speed, 0, 0);
        else if (dir == Direction.Up)
            AddPos += new Vector3(0, speed, 0);
        else if (dir == Direction.Down)
            AddPos += new Vector3(0, -speed, 0);
        else
            AddPos = Vector3.zero;

        if (CheckDir(dir))
        {
            rectTransform.localPosition += AddPos;
        }
        else
        {
            //SortingPos();
            return;
        }
    }

    public void SortingPos()
    {
        StartCoroutine(FindNear());
        SPManager.ClearCheck();
    }

    public void Selected(Color color)
    {
        GetComponent<Image>().color = color;
        
    }

    public void UnSelected()
    {
        GetComponent<Image>().color = new Color(255, 255, 255, 255); // 흰색
        SortingPos();
    }

    private Vector3 ConerCheck(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                if (rectTransform.localPosition.y > maximumPos.y){
                    SortingPos();
                    //Debug.LogError("나갈려는 시도 위");
                    return Vector3.zero;
                }
                return Vector3.up;

            case Direction.Down:
                if (rectTransform.localPosition.y < -maximumPos.y){
                    SortingPos();
                    //Debug.LogError("나갈려는 시도 밑");
                    return Vector3.zero;
                }
                return Vector3.down;

            case Direction.Left:
                if (rectTransform.localPosition.x < maximumPos.x){
                    SortingPos();
                    //Debug.LogError("나갈려는 시도 왼");
                    return Vector3.zero;
                }
                return Vector3.left;

            case Direction.Right:
                if (rectTransform.localPosition.x > -maximumPos.x){
                    SortingPos();
                    //Debug.LogError("나갈려는 시도 오");
                    return Vector3.zero;
                }
                return Vector3.right;
            case Direction.None:
                return Vector3.zero;
            default:
                return Vector3.zero;
        }     
    }

    private bool CheckDir(Direction dir)
    {
        Vector3 rayDir = Vector3.zero;

        rayDir = ConerCheck(dir);

        if (rayDir == Vector3.zero)
            return false;

        RaycastHit hit;
        Debug.DrawRay(transform.position, rayDir * rayLength, Color.red);

        if (Physics.Raycast(transform.position, rayDir, rayLength, target))
        {
            //Debug.Log("Nope");
            return false;
        }

        return true;
    }

    private Direction CalculDir(Vector3 dis)
    {
        //Debug.LogWarning(dis);
        Vector3 dir2 = dis - rectTransform.localPosition;
        float result = Mathf.Atan2(dir2.x, dir2.y) * Mathf.Rad2Deg;
        if (result < 0)
            result += 360;

        Direction direction = Direction.None;

        if (result > 0 && result < 46)
            direction = Direction.Up;
        else if (result > 45 && result < 136)
            direction = Direction.Right;
        else if (result > 135 && result < 226)
            direction = Direction.Down;
        else if (result > 225 && result < 316)
            direction = Direction.Left;
        else if (result > 315 && result < 360)
            direction = Direction.Up;
        else
            direction = Direction.None;

        //Vector3 mPos = rectTransform.localPosition;
        //if (mPos.x < LUPos.x && dis.y < LUPos.y && dis.x > RDPos.x && dis.y > RDPos.y)
        //{
        //    return Direction.None;
        //}

        return direction;
    }

    private IEnumerator FindNear()
    {
        float distance = float.MaxValue;
        Vector3 nearPos = Vector3.zero;

        foreach (Vector3 item in poses)
        {
            float disToItem = Vector2.Distance(rectTransform.localPosition, item);

            if (disToItem < distance)
            {
                distance = disToItem;
                nearPos = item;
            }

            yield return null;
        }

        //Debug.Log(nearPos);
        rectTransform.localPosition = nearPos;
    }
}
