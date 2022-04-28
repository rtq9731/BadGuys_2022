using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    GameObject obj = Instantiate(new GameObject());
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] Sound soundClip;

    [SerializeField] AudioClip[] audioCilp;
    [SerializeField] AudioClip BGMCilp;

    Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();

    float sfxVolume = 0.5f;

    AudioSource soundPlayer;

    public GameObject curLoopObj = null;
    bool isLoop = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }

        soundClip = FindObjectOfType<Sound>();

        audioCilp = soundClip.mapSounds;
        BGMCilp = soundClip.mapBGMsound;

        instance = this;
        DontDestroyOnLoad(instance.gameObject);
    }

    private void Start()
    {
        soundPlayer = GetComponent<AudioSource>();

        foreach (AudioClip a in audioCilp)
        {
            audioClipDic.Add(a.name, a);
        }
    }

    public void PlaySound(string name, float a_volume = 1f)
    {
        if (audioClipDic.ContainsKey(name) == false)
        {
            Debug.Log(name + "없음");
            return;
        }
        soundPlayer.PlayOneShot(audioClipDic[name], a_volume * sfxVolume);
    }

    public void LoopSound(string name)
    {
        if (isLoop)
            return;

        if(curLoopObj == null)
        {
            GameObject loopObj = new GameObject("LoopSound");
            curLoopObj = loopObj;
            AudioSource source = loopObj.AddComponent<AudioSource>();
            source.clip = audioClipDic[name];
            source.volume = sfxVolume;
            source.loop = true;
            source.Play();
        }
        else
        {
            AudioSource source = curLoopObj.GetComponent<AudioSource>();
            source.clip = audioClipDic[name];
            source.volume = sfxVolume;
            source.loop = true;
            source.Play();
        }

        isLoop = true;
    }

    public void StopLoopSound()
    {
        if(isLoop)
        {
            AudioSource source = curLoopObj.GetComponent<AudioSource>();
            isLoop = false;
            source.Stop();
        }
    }
    public void SetLoopPitch(float pitch)
    {
        AudioSource source = curLoopObj.GetComponent<AudioSource>();
        source.pitch = pitch;
    }

    //챕터나 스테이지 전환 시 그 챕터에서 쓰는 사운드 받아오는 코드
    public void SetStageSound()
    {
        soundClip = FindObjectOfType<Sound>();

        audioCilp = soundClip.mapSounds;
        BGMCilp = soundClip.mapBGMsound;
    }
    
    public IEnumerator PlayDelaySound(float delay, string name)
    {
        yield return new WaitForSeconds(delay);

        LoopSound(name);
    }
}
