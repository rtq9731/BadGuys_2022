using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour
{
    [SerializeField] List<RectTransform> panels = new List<RectTransform>();
    [SerializeField] List<Button> btns = new List<Button>();

    public System.Action onChangePanel;

    private void Start()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            int y = i;
            btns[y].onClick.AddListener(() =>
            {
                gameObject.SetActive(true);
                RefreshPanels();
                panels[y].gameObject.SetActive(true);
            });
        }
    }

    private void RefreshPanels()
    {
        onChangePanel?.Invoke();
        panels.ForEach(x => x.gameObject.SetActive(false));
    }
}
