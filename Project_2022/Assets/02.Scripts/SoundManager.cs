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
                if(Instance == null)
                {
                    GameObject obj = Instantiate(new GameObject());
                    instance = obj.AddComponent<SoundManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] AudioClip[] audioCilp;
    [SerializeField] AudioClip BGMCilp;

    Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();

    float sfxVolume = 0.5f;

    AudioSource soundPlayer;

    public GameObject curLoopObj = null;
    bool isLoop = false;
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
            Debug.Log(name + "¾øÀ½");
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
}
