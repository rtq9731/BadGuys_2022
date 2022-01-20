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

    private Vector3 targetPos; // �̵��� ��ġ

    private Outline outline = null; // �ƿ�����

    private bool isMouseClick;

    private float[] twoPoses = { -3, -1, 1, 3 }; // ����� 2�� �͵��� ������ ������ ��ǥ��
    private float[] threePoses = { 2, 0, -2 }; // ����� 3�� �͵��� ������ ������ ��ǥ��

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Start()
    {
        outline.enabled = false;
        Unable();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.forward * 2.5f, Color.white);
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
        //Vector3 mousePos = transform.InverseTransformVector(_mousePos);
        //Debug.Log(mousePos);

        if (isHorizontal)
        {
            targetPos = new Vector3(mousePos.x, 0f, 0f); // ���ηθ� �̵��ϱ� ������ X���� �޾ƿ�

            if (transform.localPosition.x > targetPos.x) direction = Direction.Left;        // ������ ����
            else if (transform.localPosition.x < targetPos.x) direction = Direction.Right; 
            else direction = Direction.None;

            if (CanMove(direction)) // �̵��� �����ϸ�
            {
                
                targetPos = new Vector3(mousePos.x, transform.localPosition.y, transform.localPosition.z);
                MoveToPos(targetPos); // ��ǥ�� �̵�
            }
        }
        else
        {
            targetPos = new Vector3(0f, 0f, mousePos.z); // ���θ� �̵��ϱ� ������ Z���� �޾ƿ�

            if (transform.localPosition.z > targetPos.z) direction = Direction.Down;        // ������ ����
            else if (transform.localPosition.z < targetPos.z) direction = Direction.Up;
            else direction = Direction.None;

            if (CanMove(direction)) // �̵��� �����ϸ�
            {
               
                targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y, mousePos.z);
                MoveToPos(targetPos); // ��ǥ�� �̵�
            }
        }
    }

    private bool CanMove(Direction dir) // �� �������� �̵��� ��������
    {
        if (dir == Direction.None)
        {
            return false;
        }

        if (!isThree) // 2
        {
            switch (dir) 
            {
                case Direction.Up:
                    if (transform.localPosition.z >= 3) // �ִ븦 �ȳѾ� ���°�?
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 3);
                        return false;
                    }
                    else if (ShotRay(Vector3.forward))
                        return false;

                    return true;

                case Direction.Down:
                    if (transform.localPosition.z <= -3)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -3);
                        return false;
                    }
                    else if (ShotRay(Vector3.back))
                        return false;

                    return true;

                case Direction.Right:
                    if (transform.localPosition.x >= 3)
                    {
                        transform.localPosition = new Vector3( 3, transform.localPosition.y, transform.localPosition.z);
                        return false;
                    }
                    else if (ShotRay(Vector3.right))
                        return false;

                    return true;

                case Direction.Left:
                    if (transform.localPosition.x <= -3)
                    {
                        transform.localPosition = new Vector3(-3, transform.localPosition.y, transform.localPosition.z);
                        return false;
                    }
                    else if (ShotRay(Vector3.left))
                        return false;

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
                    if (transform.localPosition.z >= 2)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 2);
                        return false;
                    }
                    else if (ShotRay(Vector3.forward))
                        return false;

                    return true;

                case Direction.Down:
                    if (transform.localPosition.z <= -2)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -2);
                        return false;
                    }
                    else if (ShotRay(Vector3.back))
                        return false;

                    return true;

                case Direction.Right:
                    if (transform.localPosition.x >= 2)
                    {
                        transform.localPosition = new Vector3(2, transform.localPosition.y, transform.localPosition.z);
                        return false;
                    }
                    else if (ShotRay(Vector3.right))
                        return false;

                    return true;

                case Direction.Left:
                    if (transform.localPosition.x >= -2)
                    {
                        transform.localPosition = new Vector3(-2, transform.localPosition.y, transform.localPosition.z);
                        return false;
                    }
                    else if (ShotRay(Vector3.left))
                        return false;

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
            if (transform.localPosition == RushHourManger.Instance.goalPos)
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
        MoveToPos(targetPos);

        if (isplayer)
        {
            if (transform.localPosition == RushHourManger.Instance.goalPos)
            {
                RushHourManger.Instance.GameClearMan();
            }
        }

    }

    public void Unable()
    {
        gameObject.SetActive(false);
    }

    public string GetColor()
    {
        return GetComponent<Material>().name;
    }

    private void MoveToPos(Vector3 thatPos)
    {
        transform.localPosition = thatPos;
    }

    private bool ShotRay(Vector3 dir)
    {
        RaycastHit hit;
        float size = RushHourManger.Instance.truesize;
        //Debug.LogWarning(size);
        //Debug.DrawRay(transform.position, dir * 2.5f * size, Color.red);

        if (!isThree) // 2
        {
            if (Physics.Raycast(transform.position, dir, out hit, 2.5f * size, target))
                return true;
        }
        else // 3
        {
            if (Physics.Raycast(transform.position, dir, out hit, 3.5f * size, target))
                return true;
        }

        return false;
    }

    private IEnumerator CheckDistance() // �� �ڵ����� ���õǸ� ���� ����� ���� ��ġ�� ����
    {
        
        Vector3 moveTo = Vector3.zero;

        if (isHorizontal) // ����
        {
            float posX = transform.localPosition.x;
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

                    if (Vector3.Distance(transform.localPosition, new Vector3(threePoses[i], 0, 0)) <
                        Vector3.Distance(transform.localPosition, new Vector3(minPosX, 0, 0)) ) // �Ÿ� ���� �� ��������� �ֱ�
                    {
                        minPosX = threePoses[i];
                    }
                }

                moveTo = new Vector3(minPosX, transform.localPosition.y, transform.localPosition.z);
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

                    if (Vector3.Distance(transform.localPosition, new Vector3(twoPoses[i], 0, 0)) <
                        Vector3.Distance(transform.localPosition, new Vector3(minPosX, 0, 0)))
                    {
                        minPosX = twoPoses[i];
                    }
                }

                moveTo = new Vector3(minPosX, transform.localPosition.y, transform.localPosition.z);
            }
        }

        else // ����
        {
            float posZ = transform.localPosition.z;
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
                    if (Vector3.Distance(transform.localPosition, new Vector3( 0, 0, threePoses[i])) <
                       Vector3.Distance(transform.localPosition, new Vector3( 0, 0, minPosZ)))
                    {
                        minPosZ = threePoses[i];
                    }
                }

                moveTo = new Vector3(transform.localPosition.x, transform.localPosition.y, minPosZ);
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
                    if (Vector3.Distance(transform.localPosition, new Vector3(0, 0, twoPoses[i])) <
                       Vector3.Distance(transform.localPosition, new Vector3(0, 0, minPosZ)))
                    {
                        minPosZ = twoPoses[i];
                    }
                }
                moveTo = new Vector3(transform.localPosition.x, transform.localPosition.y, minPosZ);
            }
        }

        targetPos = moveTo;
        yield return null;
    }


}
