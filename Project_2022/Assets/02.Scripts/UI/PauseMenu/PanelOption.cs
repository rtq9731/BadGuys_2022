using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOption : MonoBehaviour
{
    [SerializeField] SliderAndInputField sliderMasterVolume;
    [SerializeField] SliderAndInputField sliderBackgorundVolume;
    [SerializeField] SliderAndInputField sliderSFXVolume;
    [SerializeField] SliderAndInputField sliderMouseSensitivity;

    private void Start()
    {
        sliderMasterVolume.inputSlider.value = SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.MASTERVOL) / 100;
        sliderBackgorundVolume.inputSlider.value = SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.BACKGROUNDVOL) / 100;
        sliderSFXVolume.inputSlider.value = SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.SFXVOL) / 100;
        sliderMouseSensitivity.inputSlider.value = SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.MOUSESENSITIVITY) / 100;

        sliderMasterVolume.onValueChange += ChangeMasterVolume;
        sliderBackgorundVolume.onValueChange += ChangeBackgroundVolume;
        sliderSFXVolume.onValueChange += ChangeSFXVolume;
        sliderMouseSensitivity.onValueChange += ChangeMouseSensitivity;
    }

    public void ChangeMasterVolume(float value)
    {
        SettingManager.Instance.Setting.Set(value, SettingManager.SettingInfo.SettingType.MASTERVOL);
    }

    public void ChangeBackgroundVolume(float value)
    {
        SettingManager.Instance.Setting.Set(value, SettingManager.SettingInfo.SettingType.BACKGROUNDVOL);
    }

    public void ChangeSFXVolume(float value)
    {
        SettingManager.Instance.Setting.Set(value, SettingManager.SettingInfo.SettingType.SFXVOL);
    }

    public void ChangeMouseSensitivity(float value)
    {
        SettingManager.Instance.Setting.Set(value, SettingManager.SettingInfo.SettingType.MOUSESENSITIVITY);
    }
}
