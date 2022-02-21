using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionRatio : MonoBehaviour
{
    [SerializeField] Dropdown dropDown = null;
    List<Resolution> resolutions = new List<Resolution>();

    private void Start()
    {
        dropDown.onValueChanged.AddListener(OnChangeValueScreenRatio);
        resolutions = Screen.resolutions.ToList();


        Dropdown.OptionDataList optionDatas = new Dropdown.OptionDataList();
        for (int i = 0; i < resolutions.Count; i++)
        {
            optionDatas.options.Add(new Dropdown.OptionData($"{resolutions[i].width} X {resolutions[i].height} {resolutions[i].refreshRate} hz"));

            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                dropDown.value = i;
            }
        }

        dropDown.AddOptions(optionDatas.options);
    }

    void OnChangeValueScreenRatio(int value)
    {

    }

    void OnChangeValueFullScreenToggle()
    {

    }

    public void CheckInput(bool value)
    {

    }
}
