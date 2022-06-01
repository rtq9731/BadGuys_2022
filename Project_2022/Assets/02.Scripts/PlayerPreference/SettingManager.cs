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
    static SettingManager _instance = null;

    static public SettingManager Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = Instantiate<SettingManager>(Resources.Load<SettingManager>("SettingManager"));
            }

            return _instance;
        }
    }

    private static SettingInfo _setting = new SettingInfo();
    public SettingInfo Setting
    {
        get
        {
            return _setting;
        }
    }
    public static System.Action<SettingInfo> onChangeSetting;

    static readonly string fileName = "settings.sav";

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(_instance);

        LoadSettingInfo();

        _setting.onChangeValue += (setting) => onChangeSetting?.Invoke(setting);
        _setting.onChangeValue += (Setting) => SaveSettingInfo();
    }

    private void Start()
    {
        onChangeSetting?.Invoke(_setting);
    }

    public void SaveSettingInfo()
    {
        using (StreamWriter sw = new StreamWriter(Application.persistentDataPath + fileName))
        {
            sw.Write(JsonUtility.ToJson(_setting));
            Debug.Log(JsonUtility.ToJson(_setting));
        }
    }

    public void LoadSettingInfo()
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
                Debug.Log(sr.ReadToEnd());
            }
        }
    }

    [System.Serializable]
    public class SettingInfo
    {
        public enum SettingType
        {
            MASTERVOL,
            BACKGROUNDVOL,
            SFXVOL,
            MOUSESENSITIVITY
        }

        public event System.Action<SettingInfo> onChangeValue;

        [SerializeField] private float masterVolume = 100f;
        [SerializeField] private float backgroundVolume = 100f;
        [SerializeField] private float sfxVolume = 100f;
        [SerializeField] private float mouseSensitivity = 25f;

        public float GetValue(SettingType type)
        {
            switch (type)
            {
                case SettingType.MASTERVOL:
                    return masterVolume;
                case SettingType.BACKGROUNDVOL:
                    return backgroundVolume;
                case SettingType.SFXVOL:
                    return sfxVolume;
                case SettingType.MOUSESENSITIVITY:
                    return mouseSensitivity;
                default:
                    return 0f;
            }
        }

        public void Set(float value, SettingType type)
        {

            switch (type)
            {
                case SettingType.MASTERVOL:
                    masterVolume = value;
                    break;
                case SettingType.BACKGROUNDVOL:
                    backgroundVolume = value;
                    break;
                case SettingType.SFXVOL:
                    sfxVolume = value;
                    break;
                case SettingType.MOUSESENSITIVITY:
                    mouseSensitivity = value;
                    break;
                default:
                    break;
            }
            onChangeValue?.Invoke(this);

        }
    }
}