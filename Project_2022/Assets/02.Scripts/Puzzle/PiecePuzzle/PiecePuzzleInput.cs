using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PiecePuzzleInput : MonoBehaviour
{
    [SerializeField]
    private GameObject virCam;
    private GameObject parent;
    private Camera viewCamera;
    [SerializeField]
    private PiecePuzzleManager manager;
    [SerializeField]
    private LayerMask target;
    private PiecePuzzlePiece targetPiece;
    public Color color;

    private BoxCollider boxCollider;
    private Vector3 boxSize;
    private void Awake()
    {
        viewCamera = Camera.main;
        manager.pieceIn.AddListener(ChaingeTarget);
        boxCollider = GetComponent<BoxCollider>();
        boxSize = boxCollider.size - new Vector3(0.3f, 0.3f, 0);
        
    }
    float pieceSize = 1f;
    private void Update()
    {
        if (Input.GetMouseButton(0) && manager.pieceCanMove)
        {
            Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane,target))
            {
                //Debug.Log(hit.point);
                //Debug.Log(transform.position);
                
                Vector3 disPos = (hit.point - transform.position) * 2;
                //Debug.Log(disPos);
                //Vector3 dis = transform.InverseTransformVector(hit.point);
                //Debug.Log(dis);
                //targetPiece.MoveToPos(disPos);
                Vector3 localPos = transform.InverseTransformPoint(hit.point);
                // 도형 가로, 세로 구하고 /2
                //localPos.x = Mathf.Clamp(localPos.x, -boxSize.x / 2 + pieceSize / 2, boxSize.x / 2 - pieceSize / 2);
                //localPos.y = Mathf.Clamp(localPos.y, -boxSize.y / 2 + pieceSize / 2, boxSize.y / 2 - pieceSize / 2);
                targetPiece.LineOn();
                targetPiece.MoveToPos(localPos);
            }
        }

        if (Input.GetMouseButtonUp(0) && manager.pieceCanMove)
        {
            targetPiece.LineOff();
            targetPiece.CheckPieceIn();
        }
    }

    public void ChaingeTarget()
    {
        targetPiece = manager.targetPiece.transform.GetComponent<PiecePuzzlePiece>();
    }
}
