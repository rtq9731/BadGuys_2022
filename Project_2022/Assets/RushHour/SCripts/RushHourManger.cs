using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RushHourManger : MonoBehaviour
{
    public static RushHourManger Instance;

    private void Awake()
    {
        Instance = this;
    }

    
    public Vector3 goalPos;   // �������� ������������ ���� ��ǥ
    public Vector3 mousePos; // ���콺 ��ġ ����
    public LayerMask target; // �ڵ����� ���̾�
    public Car selectedCar;  // ���õ� �ڵ���
    public float outLineWidth = 10f; // �ƿ����� �α�
    public GameObject ClearPanel; // ���� Ŭ���� ȭ��


    private void Start()
    {
        ClearPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float depth = Camera.main.farClipPlane;
            //Debug.DrawRay(Camera.main.transform.position, Input.mousePosition, Color.blue, 10);
            if (Physics.Raycast(camRay, out hit, depth, target))
            {


                mousePos = hit.point;
                Car hitCar = hit.transform.GetComponent<Car>();
                //hit.transform.GetComponent<MeshRenderer>().material.color = Color.black;

                if (selectedCar != hitCar && selectedCar != null)
                {
                    selectedCar.UnSelected();
                    hitCar.Selected();
                }
                else if (selectedCar == null)
                {
                    hitCar.Selected();
                }
                else if (selectedCar == hitCar)
                {
                    selectedCar.GetMouseBtn(mousePos);
                }
            }
            else
            {
                if(selectedCar != null) selectedCar.UnSelected();
            }
        }
        

        //if (Input.GetMouseButton(0))
        //{
            
        //}
    }

    private void OnDrawGizmos() //���콺 Ŭ�� ��ġ�� ���� �׷���.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mousePos, 0.5f); 
    }

    public void GameClearMan()
    {
        ClearPanel.SetActive(true);
    }

    public void Btn_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
