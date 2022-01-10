using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private LayerMask target;
    public Direction direction;
    
    [SerializeField]
    private bool isplayer;     // �÷��̾� ���ΰ�?
    [SerializeField]
    private bool isHorizontal; // ���� ������ ������ �����ΰ�?
    [SerializeField]
    private bool isThree;      // 3ĭ ¥�� ���ΰ�?
    
    private bool isMouseClick; // ���콺 Ŭ���� �޾Ҵ°�?
    private bool isNoObstacle; // ��ֹ��� �ִ°�?

    private Vector3 targetPos; // �̵��� ��ġ

    private Outline outline = null; // �ƿ�����

    private float[] twoPoses = { -3, -1, 1, 3 }; // ����� 2�� �͵��� ������ ������ ��ǥ��
    private float[] threePoses = { 2, 0, -2 }; // ����� 3�� �͵��� ������ ������ ��ǥ��

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Start()
    {
        outline.enabled = false;
    }

    private void OnOutLine() // �ƿ����� Ŵ
    {
        outline.enabled = true;
        outline.OutlineWidth = RushHourManger.Instance.outLineWidth;
    }

    private void OffOutLine() // �ƿ����� ��
    {
        outline.enabled = false;
    }

    private void MoveToMousePos(Vector3 mousePos)
    {
        if (isHorizontal)
        {
            targetPos = new Vector3(mousePos.x, 0f, 0f); // ���ηθ� �̵��ϱ� ������ X���� �޾ƿ�

            if (transform.position.x > targetPos.x) direction = Direction.Left;        // ������ ����
            else if (transform.position.x < targetPos.x) direction = Direction.Right; 
            else direction = Direction.Left;

            if (CanMove(direction)) // �̵��� �����ϸ�
            {
                
                targetPos = new Vector3(mousePos.x, transform.position.y, transform.position.z);
                transform.position = targetPos; // ��ǥ�� �̵�
            }
        }
        else
        {
            targetPos = new Vector3(0f, 0f, mousePos.z); // ���θ� �̵��ϱ� ������ Z���� �޾ƿ�

            if (transform.position.z > targetPos.z) direction = Direction.Down;        // ������ ����
            else if (transform.position.z < targetPos.z) direction = Direction.Up;
            else direction = Direction.Down;

            if (CanMove(direction)) // �̵��� �����ϸ�
            {
               
                targetPos = new Vector3(transform.position.x, transform.position.y, mousePos.z);
                transform.position = targetPos; // ��ǥ�� �̵�
            }
        }
    }

    private bool CanMove(Direction dir) // �� �������� �̵��� ��������
    {
        
        RaycastHit hit;

        if (!isThree) // 2
        {
            switch (dir) 
            {
                case Direction.Up:
                    if (transform.position.z >= 3) // �ִ븦 �ȳѾ� ���°�?
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 3);
                        return false;
                    }
                    else if (Physics.Raycast(transform.position, Vector3.forward, out hit, 2.5f, target)) // �տ� ���� �ִ°�?
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

    public void GetMouseBtn(Vector3 mousePos) // �̵��Ǵ� ������ Ŭ���Ǹ�
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

    public void Selected() // Ŭ�� �Ǹ�
    {
        RushHourManger.Instance.selectedCar = this;
        
        OnOutLine();
    }

    public void UnSelected() // �� �ڵ����� Ŭ���ϸ�
    {
        RushHourManger.Instance.selectedCar = null;
        OffOutLine();
        StartCoroutine(CheckDistance());
        //StartCoroutine(Move(targetPos));
        transform.position = targetPos;

    }

    private IEnumerator CheckDistance() // �� �ڵ����� ���õǸ� ���� ����� ���� ��ġ�� ����
    {
        
        Vector3 moveTo = Vector3.zero;

        if (isHorizontal) // ����
        {
            float posX = transform.position.x;
            float minPosX = float.MaxValue;

            if (isThree) // ���� 3
            {
                for (int i = 0; i < threePoses.Length; i++) // ��ǥ�� �Ÿ� ��
                {
                    if (minPosX == float.MaxValue) // ���� �ȵ� ������ �ٷ� �ֱ�
                    {
                        minPosX = threePoses[i];
                        continue;
                    }

                    if (Vector3.Distance(transform.position, new Vector3(threePoses[i], 0, 0)) <
                        Vector3.Distance(transform.position, new Vector3(minPosX, 0, 0)) ) // �Ÿ� ���� �� ��������� �ֱ�
                    {
                        minPosX = threePoses[i];
                    }
                }

                moveTo = new Vector3(minPosX, transform.position.y, transform.position.z);
            }
            else // ���� 2
             {
                for (int i = 0; i < twoPoses.Length; i++) // ��ǥ�� �Ÿ� ��
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

        else // ����
        {
            float posZ = transform.position.z;
            float minPosZ = float.MaxValue;

            if (isThree) // ���� 3
            {
                for (int i = 0; i < threePoses.Length; i++) // ��ǥ�� �Ÿ� ��
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
            else // ���� 2
            {
                for(int i = 0; i < twoPoses.Length; i++) // ��ǥ�� �Ÿ� ��
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
