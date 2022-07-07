using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpsideLockPick : MonoBehaviour
{
    public GameObject targetObj;
    public float duration;
    [SerializeField]
    private LayerMask targetLay;

    private bool isClear;

    private void Awake()
    {
        isClear = false;
    }

    public void PuzzleStart()
    {
        isClear = false;
        StartCoroutine(Rotate());
    }

    public void PuzzleOver()
    {
        isClear = true;
    }

    IEnumerator Rotate()
    {
        Vector3 mousePos = new Vector3();
        Vector2 resultPos = new Vector3();
        float deg = 0f;

        while (true)
        {
            yield return null;

            if (!GameManager.Instance.IsPause)
            {
                transform.position = targetObj.transform.position;

                if (Input.GetMouseButton(0))
                {
                    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    float depth = Camera.main.farClipPlane;
                    Debug.DrawRay(Camera.main.transform.position, camRay.direction);
                    if (Physics.Raycast(camRay, out hit, depth, targetLay))
                    {
                        mousePos = hit.point;
                        resultPos = transform.position - mousePos;

                        deg = Mathf.Atan2(resultPos.y, resultPos.x) * Mathf.Rad2Deg;
                        deg += 90;

                        if (deg > 90)
                        {
                            continue;
                        }

                        deg = Mathf.Clamp(deg, -85, 85);
                        transform.DORotate(new Vector3(0, 0, deg), duration);
                    }

                }

                if (isClear)
                {
                    Debug.Log("½ÇÇà³¡");
                    break;
                }
            }  
        }

        yield return null;
    }
}
