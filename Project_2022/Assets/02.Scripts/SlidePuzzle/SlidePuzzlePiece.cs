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

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        SPManager = transform.GetComponentInParent<SlidePuzzleManager>();
        target = SPManager.target;
        speed = SPManager.pieceSpeed;
        maximumPos = SPManager.MaxPos;
    }
    private void Start()
    {
        rayLength = SPManager.rayLength;
        poses = SPManager.GetPoses();
    }

    private void Update()
    {
        //rayLength = SPManager.rayLength;
        //Debug.DrawRay(transform.position, Vector3.left * rayLength, Color.red);
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
        GetComponent<Image>().color = new Color(255, 255, 255, 255); // Èò»ö
        SortingPos();
    }

    private bool CheckDir(Direction dir)
    {
        Vector3 rayDir = Vector3.zero;

        if (dir == Direction.Up)
            rayDir = Vector3.up;
        else if (dir == Direction.Down)
            rayDir = Vector3.down;
        else if (dir == Direction.Left)
            rayDir = Vector3.left;
        else if (dir == Direction.Right)
            rayDir = Vector3.right;
        else
            return false;

        RaycastHit hit;
        Debug.DrawRay(transform.position, rayDir * rayLength, Color.red);

        if (Physics.Raycast(transform.position, rayDir, rayLength, target))
        {
            //Debug.Log("Nope");
            return false;
        }
            

        //Vector3 cPos = rectTransform.localPosition;
        //Debug.Log(rectTransform.localPosition);
        //if (dir == Direction.Left && cPos.x <= -maximumPos.x)
        //    return false;
        //if (dir == Direction.Right && cPos.x >= maximumPos.x)
        //    return false;
        //if (dir == Direction.Up && cPos.y <= -maximumPos.y)
        //    return false;
        //if (dir == Direction.Left && cPos.y >= maximumPos.y)
        //    return false;


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
