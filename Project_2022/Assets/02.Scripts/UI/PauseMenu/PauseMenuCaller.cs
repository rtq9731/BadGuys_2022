using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseMenuCaller : MonoBehaviour
{
    [SerializeField] List<RectTransform> panels;
    [SerializeField] List<PanelGroup> panelGroups;

    public enum PaenlGroupKind
    {
        PAUSEGROUP,
        OPTIONSELECTGROUP,
        OPTIONGROUP,
        DIALOGGROUP
    }

    private void OnEnable()
    {
        CallPanelGroupImmediately(PaenlGroupKind.PAUSEGROUP);
    }

    private void CallPanelGroupImmediately(PaenlGroupKind kind)
    {
        List<RectTransform> rects = panels.FindAll(item => gameObject.activeSelf && !panelGroups[(int)kind].panles.Contains(item));

        foreach (var item in rects)
        {
            item.localScale = new Vector3(0, 1, 1);
            item.gameObject.SetActive(false);
        }

        foreach (var item in panelGroups[(int)kind].panles)
        {
            item.localScale = new Vector3(1, 1, 1);
            item.gameObject.SetActive(true);
        }
    }

    public void CallPanelGroup(PaenlGroupKind kind)
    {

        Sequence seq = DOTween.Sequence();

        List<RectTransform> rects = panels.FindAll(item => gameObject.activeSelf && !panelGroups[(int)kind].panles.Contains(item));

        rects.Sort((x, y) => y.GetSiblingIndex().CompareTo(x.GetSiblingIndex()));

        foreach (var item in rects)
        {
            seq.Append(item.DOScaleX(0, 0.05f));
        }

        seq.OnComplete(() =>
        {
            foreach (var item in rects)
            {
                item.gameObject.SetActive(false);
            }

            Sequence seq2 = DOTween.Sequence();
            foreach (var item in panelGroups[(int)kind].panles)
            {
                item.gameObject.SetActive(true);
                seq2.Append(item.DOScaleX(1, 0.05f));
            }
        });
    }

    public void CallPanelGroup(int kind)
    {
        CallPanelGroup((PaenlGroupKind)kind);
    }

    [System.Serializable]
    class PanelGroup
    {
        public RectTransform[] panles;
    }

}
