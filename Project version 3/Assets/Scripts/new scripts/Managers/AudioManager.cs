using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource BGM_Source, SFX_Source;
    public Sound[] SFX;
    public Sound[] BGM;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(SFX, sfx => sfx.name == name);

        if (s == null)
        {
            Debug.Log("SFX "+name+" doesnt exist!");
            return;
        }
        
        s.source = SFX_Source;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void PlayBgm(string name)
    {
        Sound s = Array.Find(BGM, bgm => bgm.name == name);

        if (s == null)
        {
            Debug.Log("BGM "+name+" doesnt exist!");
            return;
        }
        
        s.source = BGM_Source;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.loop = s.loop;
        s.source.Play();
    }

    public void StopBgm()
    {
        BGM_Source.Stop();
    }



    // Sound Class
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1;
        [HideInInspector]
        public AudioSource source;
        public bool loop;
    }
}
