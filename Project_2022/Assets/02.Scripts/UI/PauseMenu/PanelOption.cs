using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour
{
    [SerializeField] Text optionTitle;
    [SerializeField] List<RectTransform> panels = new List<RectTransform>();
    [SerializeField] List<Button> btns = new List<Button>();
    [SerializeField] string[] titles;
    [SerializeField] PauseMenuCaller caller = null;

    private void Start()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            int y = i;
            btns[y].onClick.AddListener(() =>
            {
                caller.CallPanelGroup(PauseMenuCaller.PaenlGroupKind.OPTIONGROUP);
                gameObject.SetActive(true);
                RefreshPanels();
                optionTitle.text = titles[y];
                panels[y].gameObject.SetActive(true);
            });
        }
    }

    private void RefreshPanels()
    {
        panels.ForEach(x => x.gameObject.SetActive(false)); 
    }
}
