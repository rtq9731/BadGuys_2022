using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance = null;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject obj = Instantiate(new GameObject("SoundManager"));
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    List<SoundScript> sounds = new List<SoundScript>();

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Instance가 비지 않은 상태에서 SoundManager가 생성되었습니다!");
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(instance);
    }


    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        SettingManager.onChangeSetting += OnChangeVolume;
    }

    public void PauseAllSound()
    {
        foreach (var item in sounds)
        {
            item.Pause();
        }
    }

    public void PauseFootSound()
    {
        GameObject.Find("Player").GetComponent<PlayerFootstepSound>().PauseSound();
    }

    public void ResumeAllSound()
    {
        foreach (var item in sounds)
        {
            item.Resume();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sounds.Clear();
        foreach (var item in FindObjectsOfType<SoundScript>(true))
        {
            sounds.Add(item);
        }
        OnChangeVolume(SettingManager.Instance.Setting);
    }

    private void OnChangeVolume(SettingManager.SettingInfo setting)
    {
        for (int i = 0; i < 3; i++)
        {
            float masterVolumeValue = setting.GetValue(SettingManager.SettingInfo.SettingType.MASTERVOL);
            float volumeValue = setting.GetValue((SettingManager.SettingInfo.SettingType)i);

            foreach (var item in sounds.FindAll((item) => (int)item.audioType == i))
            {
                item.audioSource.volume = (item.originVolume * (volumeValue / 100)) * (masterVolumeValue / 100);
            }
        }
    }
}
