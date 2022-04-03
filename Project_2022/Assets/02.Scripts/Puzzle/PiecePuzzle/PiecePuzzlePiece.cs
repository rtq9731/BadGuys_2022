using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PiecePuzzlePiece : MonoBehaviour
{
    private PiecePuzzleManager manager;
    [SerializeField]
    private Vector3 myClearPos;
    private float durTime;
    private float clearDis;
    private MeshRenderer myMesh;
    private Material myMat;

    private void Awake()
    {
        manager = GetComponentInParent<PiecePuzzleManager>();
        durTime = manager.durationTime;
        clearDis = manager.clearDistance;
        myMesh = GetComponent<MeshRenderer>();
        myMat = myMesh.material;
        myMat.SetFloat("_DissolveAmount", 0);
    }

    private void Start()
    {
        myClearPos = manager.FindPiecePos(gameObject);
    }

    public void MoveToPos(Vector3 pos)
    {
        //Vector3 disPos = transform.InverseTransformVector(pos);
        //Debug.LogWarning(disPos);
        //transform.DOLocalMove(disPos, durTime);

        Debug.LogWarning(new Vector3(pos.x, pos.y, 0));
        
        transform.localPosition = new Vector3(pos.x, pos.y, -0.1f);
    }

    private void PieceIn()
    {
        manager.PutPieceIn();
        transform.DOKill();
        transform.DOLocalMove(myClearPos + new Vector3(0, 0, 0), 0);
    }

    public void CheckPieceIn()
    {
        Vector3 mPos = transform.localPosition - new Vector3(0, 0, transform.localPosition.z);
        float distance = Vector3.Distance(mPos, myClearPos);
        Debug.Log(distance);
        if (distance < clearDis)
            PieceIn();
    }

    public void AppearPiece()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        myMat.DOFloat(1, "_DissolveAmount", manager.appearTime);
        yield return new WaitForSeconds(manager.appearTime);
        manager.pieceCanMove = true;
    }
}
