using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private LayerMask target;
    public Direction direction;
    
    [SerializeField]
    private bool isplayer;     // 플레이어 차인가?
    [SerializeField]
    private bool isHorizontal; // 차가 놓여진 방향이 가로인가?
    [SerializeField]
    private bool isThree;      // 3칸 짜리 차인가?
    
    private bool isMouseClick; // 마우스 클릭을 받았는가?
    private bool isNoObstacle; // 장애물이 있는가?

    private Vector3 targetPos; // 이동할 위치

    private Outline outline = null; // 아웃라인

    private float[] twoPoses = { -3, -1, 1, 3 }; // 사이즈가 2인 것들이 스폿에 맞춰진 좌표들
    private float[] threePoses = { 2, 0, -2 }; // 사이즈가 3인 것들이 스폿에 맞춰진 좌표들

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Start()
    {
        outline.enabled = false;
    }

    private void OnOutLine() // 아웃라인 킴
    {
        outline.enabled = true;
        outline.OutlineWidth = RushHourManger.Instance.outLineWidth;
    }

    private void OffOutLine() // 아웃라인 끔
    {
        outline.enabled = false;
    }

    private void MoveToMousePos(Vector3 mousePos)
    {
        if (isHorizontal)
        {
            targetPos = new Vector3(mousePos.x, 0f, 0f); // 가로로만 이동하기 때문에 X값만 받아옴

            if (transform.position.x > targetPos.x) direction = Direction.Left;        // 방향을 정함
            else if (transform.position.x < targetPos.x) direction = Direction.Right; 
            else direction = Direction.Left;

            if (CanMove(direction)) // 이동이 가능하면
            {
                
                targetPos = new Vector3(mousePos.x, transform.position.y, transform.position.z);
                transform.position = targetPos; // 좌표로 이동
            }
        }
        else
        {
            targetPos = new Vector3(0f, 0f, mousePos.z); // 세로만 이동하기 때문에 Z값만 받아옴

            if (transform.position.z > targetPos.z) direction = Direction.Down;        // 방향을 정함
            else if (transform.position.z < targetPos.z) direction = Direction.Up;
            else direction = Direction.Down;

            if (CanMove(direction)) // 이동이 가능하면
            {
               
                targetPos = new Vector3(transform.position.x, transform.position.y, mousePos.z);
                transform.position = targetPos; // 좌표로 이동
            }
        }
    }

    private bool CanMove(Direction dir) // 그 방향으로 이동이 가능한지
    {
        
        RaycastHit hit;

        if (!isThree) // 2
        {
            switch (dir) 
            {
                case Direction.Up:
                    if (transform.position.z >= 3) // 최대를 안넘어 갔는가?
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 3);
                        return false;
                    }
                    else if (Physics.Raycast(transform.position, Vector3.forward, out hit, 2.5f, target)) // 앞에 차가 있는가?
                    {
                        return false;
                    }
                    return true;

                case Direction.Down:
                    if (transform.position.z <= -3)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, -3);
                        return false;
                    }
                    else if (Physics.Raycast(transform.position, Vector3.back, out hit, 2.5f, target))
                    {
                        return false;
                    }
                    return true;

                case Direction.Right:
                    if (transform.position.x >= 3)
                    {
                        transform.position = new Vector3( 3, transform.position.y, transform.position.z);
                        return false;
                    }
                    else if (Physics.Raycast(transform.position, Vector3.right, out hit, 2.5f, target))
                    {
                        return false;
                    }
                    return true;

                case Direction.Left:
                    if (transform.position.x <= -3)
                    {
                        transform.position = new Vector3(-3, transform.position.y, transform.position.z);
                        return false;
                    }
                    else if (Physics.Raycast(transform.position, Vector3.left, out hit, 2.5f, target))
                    {
                        return false;
                    }
                    return true;

                default:
                    return false;
            }

        }
        else // 3
        {
            switch (dir)
            {
                case Direction.Up:
                    if (transform.position.z >= 2)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                        return false;
                    }
                    if (Physics.Raycast(transform.position, Vector3.forward, out hit, 3.5f, target))
                    {
                        return false;
                    }
                    return true;

                case Direction.Down:
                    if (transform.position.z <= -2)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
                        return false;
                    }
                    if (Physics.Raycast(transform.position, Vector3.back, out hit, 3.5f, target))
                    {
                        return false;
                    }
                    return true;

                case Direction.Right:
                    if (transform.position.x >= 2)
                    {
                        transform.position = new Vector3(2, transform.position.y, transform.position.z);
                        return false;
                    }
                    if (Physics.Raycast(transform.position, Vector3.right, out hit, 3.5f, target))
                    {
                        return false;
                    }
                    return true;

                case Direction.Left:
                    if (transform.position.x >= -2)
                    {
                        transform.position = new Vector3(-2, transform.position.y, transform.position.z);
                        return false;
                    }
                    if (Physics.Raycast(transform.position, Vector3.left, out hit, 3.5f, target))
                    {
                        return false;
                    }
                    return true;

                default:
                    return false;
            }

        }
    }

    public void GetMouseBtn(Vector3 mousePos) // 이동되는 곳으로 클릭되면
    {
        isMouseClick = true;
        MoveToMousePos(mousePos);

        if (isplayer)
        {
            if (transform.position == RushHourManger.Instance.goalPos)
            {
                RushHourManger.Instance.GameClearMan();
            }
        }
    }

    public void Selected() // 클릭 되면
    {
        RushHourManger.Instance.selectedCar = this;
        
        OnOutLine();
    }

    public void UnSelected() // 딴 자동차를 클릭하면
    {
        RushHourManger.Instance.selectedCar = null;
        OffOutLine();
        StartCoroutine(CheckDistance());
        //StartCoroutine(Move(targetPos));
        transform.position = targetPos;

    }

    private IEnumerator CheckDistance() // 딴 자동차가 선택되면 가장 가까운 스폿 위치로 맞춤
    {
        
        Vector3 moveTo = Vector3.zero;

        if (isHorizontal) // 가로
        {
            float posX = transform.position.x;
            float minPosX = float.MaxValue;

            if (isThree) // 길이 3
            {
                for (int i = 0; i < threePoses.Length; i++) // 좌표간 거리 비교
                {
                    if (minPosX == float.MaxValue) // 값이 안들어가 있으면 바로 넣기
                    {
                        minPosX = threePoses[i];
                        continue;
                    }

                    if (Vector3.Distance(transform.position, new Vector3(threePoses[i], 0, 0)) <
                        Vector3.Distance(transform.position, new Vector3(minPosX, 0, 0)) ) // 거리 비교후 더 가까운쪽을 넣기
                    {
                        minPosX = threePoses[i];
                    }
                }

                moveTo = new Vector3(minPosX, transform.position.y, transform.position.z);
            }
            else // 길이 2
             {
                for (int i = 0; i < twoPoses.Length; i++) // 좌표간 거리 비교
                {
                    if (minPosX == float.MaxValue)
                    {
                        minPosX = twoPoses[i];
                        continue;
                    }

                    if (Vector3.Distance(transform.position, new Vector3(twoPoses[i], 0, 0)) <
                        Vector3.Distance(transform.position, new Vector3(minPosX, 0, 0)))
                    {
                        minPosX = twoPoses[i];
                    }
                }

                moveTo = new Vector3(minPosX, transform.position.y, transform.position.z);
            }
        }

        else // 세로
        {
            float posZ = transform.position.z;
            float minPosZ = float.MaxValue;

            if (isThree) // 길이 3
            {
                for (int i = 0; i < threePoses.Length; i++) // 좌표간 거리 비교
                {
                    if (minPosZ == float.MaxValue)
                    {
                        minPosZ = threePoses[i];
                        continue;
                    }
                    if (Vector3.Distance(transform.position, new Vector3( 0, 0, threePoses[i])) <
                       Vector3.Distance(transform.position, new Vector3( 0, 0, minPosZ)))
                    {
                        minPosZ = threePoses[i];
                    }
                }

                moveTo = new Vector3(transform.position.x, transform.position.y, minPosZ);
            }
            else // 길이 2
            {
                for(int i = 0; i < twoPoses.Length; i++) // 좌표간 거리 비교
                {
                    if (minPosZ == float.MaxValue)
                    {
                        minPosZ = twoPoses[i];
                        continue;
                    }
                    if (Vector3.Distance(transform.position, new Vector3(0, 0, twoPoses[i])) <
                       Vector3.Distance(transform.position, new Vector3(0, 0, minPosZ)))
                    {
                        minPosZ = twoPoses[i];
                    }
                }
                moveTo = new Vector3(transform.position.x, transform.position.y, minPosZ);
            }
        }

        targetPos = moveTo;
        yield return null;
    }


}
