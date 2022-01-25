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
        viewCamera = Camera.main;
    }

    
    public Vector3 goalPos;   // 도착지의 도착했을때의 차의 좌표
    public Vector3 mousePos; // 마우스 위치 변수
    private Vector3 originPos; // 월드 좌표
    public LayerMask target; // 자동차들 레이어
    public Car selectedCar;  // 선택된 자동차
    public float outLineWidth = 10f; // 아웃라인 두깨

    private Camera viewCamera;

    public float truesize; // 현재 보드사이즈 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray camRay = viewCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float depth = Camera.main.farClipPlane;
            
            if (Physics.Raycast(camRay, out hit, depth, target))
            {
                Vector3 mePos = transform.position;
                mousePos = transform.InverseTransformVector(hit.point - new Vector3(mePos.x, 0, mePos.z));
                originPos = hit.point;
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
            else if (selectedCar != null)
            {
                selectedCar.UnSelected();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedCar != null) selectedCar.UnSelected();
        }
    }

    private void OnDrawGizmos() //마우스 클릭 위치에 원을 그려줌.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(originPos, 0.05f); 
    }

    public void GameClearMan()
    {
        if (selectedCar != null)
            selectedCar.UnSelected();
        GetComponent<Item_RushHourPuzzle>().GameClear();
    }

    public void Btn_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CameraSet(Camera viewCam)
    {
        viewCamera = viewCam;
    }
}
