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

    
    public Vector3 goalPos;   // 도착지의 도착했을때의 차의 좌표
    public Vector3 mousePos; // 마우스 위치 변수
    public LayerMask target; // 자동차들 레이어
    public Car selectedCar;  // 선택된 자동차
    public float outLineWidth = 10f; // 아웃라인 두깨
    public GameObject ClearPanel; // 게임 클리어 화면


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

    private void OnDrawGizmos() //마우스 클릭 위치에 원을 그려줌.
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
