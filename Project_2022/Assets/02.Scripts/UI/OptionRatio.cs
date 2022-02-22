using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionRatio : MonoBehaviour
{
    [SerializeField] Dropdown ratioDropdown = null;
    [SerializeField] Dropdown fullScreenDropdown = null;

    [SerializeField] CheckInputPanel inputPanel = null;

    List<Resolution> resolutions = new List<Resolution>();
    FullScreenMode curFullScreenMode;
    Array fullscreenModes;

    Action onCancel = null;

    private void Start()
    {
        ratioDropdown.ClearOptions();
        fullScreenDropdown.ClearOptions();
        ratioDropdown.onValueChanged.AddListener(OnChangeValueScreenRatio);
        fullScreenDropdown.onValueChanged.AddListener(OnChangeValueFullscreenMode);

        resolutions = Screen.resolutions.ToList();

        Dropdown.OptionDataList optionRatioDatas = new Dropdown.OptionDataList();
        for (int i = 0; i < resolutions.Count; i++)
        {
            optionRatioDatas.options.Add(new Dropdown.OptionData($"{resolutions[i].width} X {resolutions[i].height} {resolutions[i].refreshRate} hz"));

            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                ratioDropdown.value = i;
            }
        }

        Dropdown.OptionDataList optionFullScreenModeDatas = new Dropdown.OptionDataList();

        fullscreenModes = Enum.GetValues(typeof(FullScreenMode));
        Array fullscreenModeNames = Enum.GetNames(typeof(FullScreenMode));

        for (int i = 0; i < fullscreenModes.Length; i++)
        {
            optionFullScreenModeDatas.options.Add(new Dropdown.OptionData($"{fullscreenModeNames.GetValue(i)}"));
        }

        fullScreenDropdown.AddOptions(optionFullScreenModeDatas.options);
        ratioDropdown.AddOptions(optionRatioDatas.options);
    }

    void OnChangeValueScreenRatio(int value)
    {
        curFullScreenMode = (FullScreenMode)(fullScreenDropdown.value);

        Resolution curResolution = Screen.currentResolution;

        Screen.SetResolution(resolutions[value].width, resolutions[value].height, (FullScreenMode)(fullScreenDropdown.value), resolutions[value].refreshRate);

        inputPanel.InitInputPanel(() =>
        {
            Screen.SetResolution(curResolution.width, curResolution.height, curFullScreenMode, curResolution.refreshRate);
        });
    }

    void OnChangeValueFullscreenMode(int value)
    {
        curFullScreenMode = (FullScreenMode)(fullScreenDropdown.value);

        Resolution curResolution = Screen.currentResolution;

        Screen.SetResolution(resolutions[value].width, resolutions[value].height, (FullScreenMode)(fullScreenDropdown.value), resolutions[value].refreshRate);

        inputPanel.InitInputPanel(() =>
        {
            Screen.SetResolution(curResolution.width, curResolution.height, curFullScreenMode, curResolution.refreshRate);
        });

        curFullScreenMode = (FullScreenMode)(fullScreenDropdown.value);
    }
}
