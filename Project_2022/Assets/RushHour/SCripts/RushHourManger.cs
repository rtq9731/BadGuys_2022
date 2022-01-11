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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float depth = Camera.main.farClipPlane;
            
            if (Physics.Raycast(camRay, out hit, depth, target))
            {
                Vector3 mePos = transform.position;
                mousePos = transform.InverseTransformVector(hit.point - new Vector3(mePos.x, 0, mePos.z));
                Car hitCar = hit.transform.GetComponent<Car>();

                if (selectedCar != hitCar && selectedCar != null)
                {
                    selectedCar.UnSelected();
                    hitCar.Selected();
                }
                if (selectedCar == null)
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

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedCar != null) selectedCar.UnSelected();
        }
    }

    private void OnDrawGizmos() //���콺 Ŭ�� ��ġ�� ���� �׷���.
    {
        Debug.Log(mousePos);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mousePos, 0.5f); 
    }

    public void GameClearMan()
    {
        Debug.Log("���� Ŭ����");
    }

    public void Btn_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
