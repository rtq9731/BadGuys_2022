using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleInput : MonoBehaviour
{
    [SerializeField]
    private Camera viewCamera;
    [SerializeField]
    private SlidePuzzleManager slideManager;
    [SerializeField]
    private Color color;

    private LayerMask target; // 그림들 레이어
    private SlidePuzzlePiece selectPiece;
    private Vector3 hitPos;

    private void Awake()
    {
        if (viewCamera == null) viewCamera = Camera.main;
        target = slideManager.target;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && slideManager.isPieceStop)
        {
            Ray camRay = viewCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float depth = viewCamera.farClipPlane;

            if (Physics.Raycast(camRay, out hit, depth, target))
            {
                selectPiece = hit.transform.GetComponent<SlidePuzzlePiece>();
                selectPiece.Selected(color);
                
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectPiece.MoveToDis();

            if (selectPiece != null)
                selectPiece.UnSelected();

            selectPiece = null;
        }
    }

    private void OnDrawGizmos() //마우스 클릭 위치에 원을 그려줌.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(hitPos, 0.05f);
    }
}
