using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Triggers;

public class RotationPuzzle : MonoBehaviour
{
    [SerializeField] float rotationAmount = 10;

    [SerializeField] List<RotationPuzzleElement> elements = new List<RotationPuzzleElement>();
    [SerializeField] StoryTrigger completeTrigger = null;
    [SerializeField] GStageLightTrigger lightTrigger = null;
    [SerializeField] GameObject completeWall = null;

    [SerializeField] GameObject vCamPuzzle = null;
    [SerializeField] SpriteRenderer completeSR = null;

    [SerializeField] GameObject keyPiece = null;

    bool isOn = false;

    int curLayer = 0;

    private void Update()
    {
        if (!isOn)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            curLayer = 0;
            RefreshOulines(curLayer);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            curLayer = 1;
            RefreshOulines(curLayer);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            curLayer = 2;
            RefreshOulines(curLayer);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            elements[curLayer].RotatePicture(-rotationAmount);
            OnElementRotate();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            elements[curLayer].RotatePicture(rotationAmount);
            OnElementRotate();
        }
    }

    public void SetPuzzle()
    {
        vCamPuzzle.SetActive(true);
        isOn = true;
        RefreshOulines(curLayer);
    }

    private void RefreshOulines(int curLayer)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].outline.enabled = i == curLayer;
        }
    }

    private void OnElementRotate()
    {
        Debug.Log("0 : " + elements[0].GetPictureRotationZ() % 360 + " == " + "1 : " + elements[1].GetPictureRotationZ() % 360 + " = " + (elements[0].GetPictureRotationZ() == (int)elements[1].GetPictureRotationZ()));
        Debug.Log("0 : " + elements[0].GetPictureRotationZ() % 360 + " == " + "2 : " + elements[2].GetPictureRotationZ() % 360 + " = " + (elements[0].GetPictureRotationZ() == (int)elements[2].GetPictureRotationZ()));
        if (!((int)(elements[0].GetPictureRotationZ() % 360) == (int)(elements[1].GetPictureRotationZ() % 360)
            && (int)(elements[0].GetPictureRotationZ() % 360) == (int)(elements[2].GetPictureRotationZ() % 360)))
            return;

        OnCompletePuzzle(elements[0].GetPictureRotationZ());
    }

    private void OnCompletePuzzle(float destRot)
    {
        vCamPuzzle.SetActive(true);
        completeSR.transform.rotation = Quaternion.Euler(new Vector3(0, 180, destRot));
        completeSR.gameObject.SetActive(true);
        completeSR.material.SetFloat("_DissolveAmount", 0f);
        UIManager._instance.OnCutScene();

        foreach (var item in elements)
        {
            item.DORotateToAnswer(destRot);
        }

        completeSR.material.DOFloat(1, "_DissolveAmount", 3f).OnComplete(() => 
        {
            completeSR.material.DOFloat(0, "_DissolveAmount", 3f).OnComplete(() =>
            {
                keyPiece.SetActive(true);
                keyPiece.GetComponent<Item>().Interact(keyPiece);

                UIManager._instance.OnCutSceneOverWithoutClearDialog();

                completeSR.gameObject.SetActive(false);
                completeTrigger.OnTriggered();

                vCamPuzzle.SetActive(false);

                completeWall.GetComponent<WallDissolve>().WallDissolveScene();

                //completeWall.SetActive(false);

                RefreshOulines(-1);

                lightTrigger.SetActiveGroup(true);
                enabled = false;
            });
        });

    }
}
