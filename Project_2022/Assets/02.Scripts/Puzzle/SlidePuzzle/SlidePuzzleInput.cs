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
    [SerializeField]
    private LayerMask btnLayer;
    [SerializeField]
    private SlideSound slideSound;

    private LayerMask target; // 그림들 레이어
    private SlidePuzzlePiece selectPiece;
    private Vector3 hitPos;
    private bool cheatOn;
    private bool btnClick;
   

    private void Awake()
    {
        if (viewCamera == null) viewCamera = Camera.main;
        target = slideManager.target;
    }

    void Update()
    {
        if (!GameManager.Instance.IsPause)
        {
            if (Input.GetMouseButtonDown(0) && slideManager.isPieceStop)
            {
                Ray camRay = viewCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                float depth = viewCamera.farClipPlane;

                if (Physics.Raycast(camRay, out hit, depth, target))
                {
                    selectPiece = hit.transform.GetComponent<SlidePuzzlePiece>();
                    if (selectPiece != null)
                        selectPiece.Selected(color);
                }

                if (Physics.Raycast(camRay, out hit, depth, btnLayer) && !btnClick)
                {
                    btnClick = true;
                    hit.transform.GetComponent<SlidePuzzleBtn>().Selected(color);
                    hit.transform.GetComponent<SlidePuzzleBtn>().Onclick();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (selectPiece != null)
                {
                    slideSound.SlideMove();
                    selectPiece.MoveToDis();
                    selectPiece.UnSelected();
                }

                selectPiece = null;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                cheatOn = true;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (cheatOn)
                    slideManager.PorceClear();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                if (SlidePuzzleAllClear.Instance.isWeak == false && cheatOn)
                    SlidePuzzleAllClear.Instance.slideCount += 100;
            }
        }
    }
}
