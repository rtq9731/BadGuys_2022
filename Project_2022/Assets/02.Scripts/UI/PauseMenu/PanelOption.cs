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
        sliderMasterVolume.onValueChange += ChangeMasterVolume;
        sliderBackgorundVolume.onValueChange += ChangeBackgroundVolume;
        sliderSFXVolume.onValueChange += ChangeSFXVolume;
        sliderMouseSensitivity.onValueChange += ChangeMouseSensitivity;
    }

    private void OnEnable()
    {
        sliderMasterVolume.SetValue(SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.MASTERVOL));
        sliderBackgorundVolume.SetValue(SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.BACKGROUNDVOL));
        sliderSFXVolume.SetValue(SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.SFXVOL));
        sliderMouseSensitivity.SetValue(SettingManager.Instance.Setting.GetValue(SettingManager.SettingInfo.SettingType.MOUSESENSITIVITY));
    }

    public void ChangeMasterVolume(float value)
    {
        SettingManager.Instance.Setting.SetValue(value, SettingManager.SettingInfo.SettingType.MASTERVOL);
    }

    public void ChangeBackgroundVolume(float value)
    {
        SettingManager.Instance.Setting.SetValue(value, SettingManager.SettingInfo.SettingType.BACKGROUNDVOL);
    }

    public void ChangeSFXVolume(float value)
    {
        SettingManager.Instance.Setting.SetValue(value, SettingManager.SettingInfo.SettingType.SFXVOL);
    }

    public void ChangeMouseSensitivity(float value)
    {
        SettingManager.Instance.Setting.SetValue(value, SettingManager.SettingInfo.SettingType.MOUSESENSITIVITY);
    }
}
