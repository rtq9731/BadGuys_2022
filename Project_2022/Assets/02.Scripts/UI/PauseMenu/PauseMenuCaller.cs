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
        CallPanelGroup(PaenlGroupKind.PAUSEGROUP);
    }

    public void CallPanelGroup(PaenlGroupKind kind)
    {
        Sequence seq = DOTween.Sequence();

        foreach (var item in panels.FindAll(item => gameObject.activeSelf && !panelGroups[(int)kind].panles.Contains(item)))
        {
            seq.Join(item.DOScaleX(0, 0.2f).OnComplete(() => item.gameObject.SetActive(false)));
        }

        seq.AppendInterval(0.01f);

        seq.OnComplete(() =>
        {
            foreach (var item in panelGroups[(int)kind].panles)
            {
                item.gameObject.SetActive(true);
                item.DOScaleX(1, 0.2f);
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
