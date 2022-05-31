using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum SettingKind
{
    MASTERVOLUME,
    SFXVOLUME,
    BACKGROUNDVOLUME,
    MOUSESENSITIVITY
}


public class SettingManager : MonoBehaviour
{
    private static SettingInfo _setting = new SettingInfo();
    public static SettingInfo Setting
    {
        get
        {
            onChangeSetting?.Invoke();
            return _setting;
        }
    }
    public static System.Action onChangeSetting;

    static readonly string fileName = "settings.sav";

    private void Start()
    {
        LoadSettingInfo();
    }

    public static void SaveSettingInfo()
    {
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + fileName))
        {
            sw.Write(JsonUtility.ToJson(_setting));
        }
    }

    public static void LoadSettingInfo()
    {
        using (StreamReader sr = new StreamReader(Application.persistentDataPath + fileName))
        {
            if (sr == null)
            {
                _setting = new SettingInfo();
            }
            else
            {
                _setting = JsonUtility.FromJson<SettingInfo>(sr.ReadToEnd());
            }
        }
    }
}

public class SettingInfo
{
    public float masterVolume = 100f;
    public float backgroundVolume = 100f;
    public float sfxVolume = 100f;
    public float mouseSensitivity = 25f;
    public Resolution resolution = Screen.currentResolution;
    public FullScreenMode fullScreenMode = Screen.fullScreenMode;

}