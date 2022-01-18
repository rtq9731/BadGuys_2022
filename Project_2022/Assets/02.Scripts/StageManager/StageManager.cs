using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    [SerializeField] Text changeText;
    [SerializeField] Image panelHide;

    public void StageChange()
    {
        panelHide.rectTransform.DOAnchorPosY(0, 1f);
    }
}
