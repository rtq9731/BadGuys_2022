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

    private void Start()
    {
        ratioDropdown.ClearOptions();
        fullScreenDropdown.ClearOptions();

        resolutions = Screen.resolutions.ToList();

        lastReslution = Screen.currentResolution;
        lastFullScreenMode = Screen.fullScreenMode;

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

        string[] fullScreenNames =
        {
            "전체화면",
            "테두리 없는 창모드",
            "창모드"
        };

        for (int i = 0; i < fullScreenNames.Length; i++)
        {
            optionFullScreenModeDatas.options.Add(new Dropdown.OptionData($"{fullScreenNames[i]}"));
        }

        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.ExclusiveFullScreen:
                lastFullScreenValue = 0;
                break;
            case FullScreenMode.FullScreenWindow:
                lastFullScreenValue = 1;
                break;
            case FullScreenMode.Windowed:
                lastFullScreenValue = 2;
                break;
            default:
                break;
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
        if (canChangeOption)
        {
            canChangeOption = false;

            int temp = lastRatioValue;

            Screen.SetResolution(resolutions[value].width, resolutions[value].height, (FullScreenMode)(fullScreenDropdown.value), resolutions[value].refreshRate);

            Resolution resolution = lastReslution;

            inputPanel.InitInputPanel(() =>
            {
                lastRatioValue = temp;
                ratioDropdown.value = temp;
                canChangeOption = true;
                Screen.SetResolution(lastReslution.width, lastReslution.height, lastFullScreenMode, lastReslution.refreshRate);
                lastReslution = Screen.currentResolution;
            },
            () =>
            {
                canChangeOption = true;
                lastRatioValue = value;
                lastReslution = Screen.currentResolution;
            });

        }
    }

    void OnChangeValueFullscreenMode(int value)
    {
        if(canChangeOption)
        {
            canChangeOption = false;

            FullScreenMode fullScreen = lastFullScreenMode;
            int temp = lastFullScreenValue;

            FullScreenMode targetFullScreenMode;
            switch (value)
            {
                case 0:
                    targetFullScreenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
                case 1:
                    targetFullScreenMode = FullScreenMode.FullScreenWindow;
                    break;
                case 2:
                    targetFullScreenMode = FullScreenMode.Windowed;
                    break;
                default:
                    targetFullScreenMode = FullScreenMode.FullScreenWindow;
                    break;
            }

            Screen.SetResolution(lastReslution.width, lastReslution.height, targetFullScreenMode, lastReslution.refreshRate);

            inputPanel.InitInputPanel(() =>
            {
                fullScreenDropdown.value = temp;
                canChangeOption = true;
                Screen.SetResolution(lastReslution.width, lastReslution.height, fullScreen, lastReslution.refreshRate);
                lastFullScreenMode = targetFullScreenMode;
            }, 
            () => 
            {
                canChangeOption = true;
                lastFullScreenValue = value;
                lastFullScreenMode = targetFullScreenMode;
            });
        }

    }
}
