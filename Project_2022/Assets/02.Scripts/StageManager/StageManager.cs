using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    [SerializeField] Text aiMsgText;
    [SerializeField] Text stageMsgText;
    [SerializeField] Image panelHide;

    private void Start()
    {
        StageChange();
    }

    public void StageChange()
    {
        panelHide.gameObject.SetActive(true);
        stageMsgText.text = "";
        aiMsgText.text = "";
        panelHide.rectTransform.DOAnchorPosY(-panelHide.rectTransform.rect.height, 1f).OnComplete(()=>
        {
            stageMsgText.DOText("조심해요!!!", 1f);
            stageMsgText.transform.DOShakePosition(10, 8f);
            aiMsgText.DOText("M.A.M : 새로운 기억 발견, 장소 재구성 중. . .", 5f).OnComplete(() =>
            {
                aiMsgText.text = " ";
                aiMsgText.DOText("M.A.M : 새 기억으로 업데이트된 장소로 이동합니다.", 3f).OnComplete(() =>
                {
                    panelHide.rectTransform.DOAnchorPosY(panelHide.rectTransform.rect.height / 2, 0.5f).OnComplete(() =>
                    {
                        panelHide.gameObject.SetActive(false);
                    });
                });
            });
        });
    }
}
