using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) 
        { 
            Debug.LogWarning("Sound: " + name + " not found!");
            return; 
        }
        s.source.Play();
    }

    public void Stop(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) 
        { 
            Debug.LogWarning("Sound: " + name + " not found!");
            return; 
        }
        s.source.Stop();
    }
}
