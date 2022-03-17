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

    private void Awake()
    {
        if (viewCamera == null) viewCamera = Camera.main;
        target = slideManager.target;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

        if (Input.GetMouseButton(0) && selectPiece != null)
        {
            Ray camRay = viewCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float depth = viewCamera.farClipPlane;

            if (Physics.Raycast(camRay, out hit, depth))
            {
                Vector3 mePos = transform.position;
                Vector3 mousePos = transform.InverseTransformVector(hit.point - new Vector3(mePos.x, mePos.y, hit.point.z));

                //Debug.LogWarning(mousePos);
                selectPiece.MoveToPos(mousePos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectPiece != null)
            {
                selectPiece.SortingPos();
                selectPiece.UnSelected();
            }
            selectPiece = null;
        }
    }
}
