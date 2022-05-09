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

        if (soundClip != null)
        {
            audioCilp = soundClip.mapSounds;
            BGMCilp = soundClip.mapBGMsound;

            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    private void Start()
    {
        if(soundClip != null)
        {
            soundPlayer = GetComponent<AudioSource>();

            foreach (AudioClip a in audioCilp)
            {
                audioClipDic.Add(a.name, a);
            }
        }
        
    }

    public void PlaySound(string name, float a_volume = 1f)
    {
        if (soundClip != null)
        {
            if (audioClipDic.ContainsKey(name) == false)
            {
                Debug.Log(name + "����");
                return;
            }
            soundPlayer.PlayOneShot(audioClipDic[name], a_volume * sfxVolume);
        }
    }

    public void LoopSound(string name)
    {
        if (soundClip != null)
        {
            if (isLoop)
                return;

            if (curLoopObj == null)
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
    }

    public void StopLoopSound()
    {
        if (soundClip != null)
        {
            if (isLoop)
            {
                AudioSource source = curLoopObj.GetComponent<AudioSource>();
                isLoop = false;
                source.Stop();
            }
        }
    }
    public void SetLoopPitch(float pitch)
    {
        if (soundClip != null)
        {
            AudioSource source = curLoopObj.GetComponent<AudioSource>();
            source.pitch = pitch;
        }
    }

    //é�ͳ� �������� ��ȯ �� �� é�Ϳ��� ���� ���� �޾ƿ��� �ڵ�
    public void SetStageSound()
    {
        if (soundClip != null)
        {
            soundClip = FindObjectOfType<Sound>();

            audioCilp = soundClip.mapSounds;
            BGMCilp = soundClip.mapBGMsound;
        }
    }
    
    public IEnumerator PlayDelaySound(float delay, string name)
    {
        if (soundClip != null)
        {
            yield return new WaitForSeconds(delay);

            LoopSound(name);
        }
    }
}