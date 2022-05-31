using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionRatio : MonoBehaviour
{
    bool canChangeOption = true;

    int lastRatioValue;
    int lastFullScreenValue;

    [SerializeField] Dropdown ratioDropdown = null;
    [SerializeField] Dropdown fullScreenDropdown = null;

    [SerializeField] CheckInputPanel inputPanel = null;

    List<Resolution> resolutions = new List<Resolution>();

    FullScreenMode lastFullScreenMode;
    Resolution lastReslution;

    Array fullscreenModes;

    private void Start()
    {
        ratioDropdown.ClearOptions();
        fullScreenDropdown.ClearOptions();

        resolutions = Screen.resolutions.ToList();

        Dropdown.OptionDataList optionRatioDatas = new Dropdown.OptionDataList();

        for (int i = 0; i < resolutions.Count; i++)
        {
            optionRatioDatas.options.Add(new Dropdown.OptionData($"{resolutions[i].width} X {resolutions[i].height} {resolutions[i].refreshRate} hz"));

            if (Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height && Screen.currentResolution.refreshRate == resolutions[i].refreshRate)
            {
                lastRatioValue = i;
            }
        }

        Dropdown.OptionDataList optionFullScreenModeDatas = new Dropdown.OptionDataList();

        fullscreenModes = Enum.GetValues(typeof(FullScreenMode));
        Array fullscreenModeNames = Enum.GetNames(typeof(FullScreenMode));

        for (int i = 0; i < fullscreenModes.Length; i++)
        {
            optionFullScreenModeDatas.options.Add(new Dropdown.OptionData($"{fullscreenModeNames.GetValue(i)}"));

            if(Screen.fullScreenMode == (FullScreenMode)fullscreenModes.GetValue(i))
            {
                lastFullScreenValue = i;
            }
        }

        fullScreenDropdown.AddOptions(optionFullScreenModeDatas.options);
        ratioDropdown.AddOptions(optionRatioDatas.options);

        ratioDropdown.value = lastRatioValue;
        fullScreenDropdown.value = lastFullScreenValue;

        ratioDropdown.onValueChanged.AddListener(OnChangeValueScreenRatio);
        fullScreenDropdown.onValueChanged.AddListener(OnChangeValueFullscreenMode);
    }

    void OnChangeValueScreenRatio(int value)
    {
        if(canChangeOption)
        {
            canChangeOption = false;

            int temp = lastRatioValue;

            Screen.SetResolution(resolutions[value].width, resolutions[value].height, (FullScreenMode)(fullScreenDropdown.value), resolutions[value].refreshRate);

            Resolution resolution = lastReslution;

            inputPanel.InitInputPanel(() =>
            {
                ratioDropdown.value = temp;
                canChangeOption = true;
                Screen.SetResolution(lastReslution.width, lastReslution.height, lastFullScreenMode, lastReslution.refreshRate);
            }, () => canChangeOption = true);

        }

        lastRatioValue = value;
        lastReslution = Screen.currentResolution;
    }

    void OnChangeValueFullscreenMode(int value)
    {
        if(canChangeOption)
        {
            canChangeOption = false;

            FullScreenMode fullScreen = lastFullScreenMode;
            int temp = lastFullScreenValue;

            Screen.SetResolution(lastReslution.width, lastReslution.height, (FullScreenMode)(fullScreenDropdown.value), lastReslution.refreshRate);

            inputPanel.InitInputPanel(() =>
            {
                fullScreenDropdown.value = temp;
                canChangeOption = true;
                Screen.SetResolution(lastReslution.width, lastReslution.height, fullScreen, lastReslution.refreshRate);
            }, () => canChangeOption = true);
        }

        lastFullScreenValue = value;
        lastFullScreenMode = (FullScreenMode)(fullScreenDropdown.value);
    }
}
