using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour
{
    [SerializeField] Slider sliderMasterVolume;
    [SerializeField] Slider sliderBackgorundVolume;
    [SerializeField] Slider sliderSFXVolume;
    [SerializeField] Slider sliderMouseSensitivity;

    private void Start()
    {
        sliderMasterVolume.onValueChanged.AddListener(ChangeMasterVolume);
        sliderBackgorundVolume.onValueChanged.AddListener(ChangeBackgroundVolume);
        sliderSFXVolume.onValueChanged.AddListener(ChangeSFXVolume);
        sliderMouseSensitivity.onValueChanged.AddListener(ChangeMouseSensitivity);
    }

    public void ChangeMasterVolume(float value)
    {
        SettingManager.Setting.masterVolume = value;
    }

    public void ChangeBackgroundVolume(float value)
    {
        SettingManager.Setting.backgroundVolume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        SettingManager.Setting.sfxVolume = value;
    }

    public void ChangeMouseSensitivity(float value)
    {
        SettingManager.Setting.mouseSensitivity = value;
    }
}
