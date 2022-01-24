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

    public StageChangeData data;

    public void StageChange()
    {
        panelHide.gameObject.SetActive(true);
        stageMsgText.text = "";
        aiMsgText.text = "";
        UIManager._instance.OnCutScene();
        panelHide.rectTransform.DOAnchorPosY(-panelHide.rectTransform.rect.height, 2f).OnComplete(()=>
        {
            // ���ڴ� 0.125f ��
            stageMsgText.DOText("�����ؿ�!!!", 1f);
            stageMsgText.transform.DOShakePosition(10, 8f);
            aiMsgText.DOText("M.A.M : ���ο� ��� �߰�, ��� �籸�� ��. . .", 5f).OnComplete(() => // ���ڴ� 0.1f ��
            {   
                aiMsgText.text = " ";
                aiMsgText.DOText("M.A.M : �� ������� ������Ʈ�� ��ҷ� �̵��մϴ�.", 3f).OnComplete(() =>
                {
                    panelHide.rectTransform.DOAnchorPosY(panelHide.rectTransform.rect.height / 2, 0.5f).OnComplete(() =>
                    {
                        panelHide.gameObject.SetActive(false);
                        UIManager._instance.OnCutSceneOver();
                    });
                });
            });
        });
    }
}

[CreateAssetMenu(fileName = "StageDialog", menuName = "ScriptableObject/StageChangeDialog")]
public class StageChangeData : ScriptableObject
{
    public string[] aiMsgs;
    public string[] stageMsgs;
}
