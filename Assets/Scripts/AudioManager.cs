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

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(String name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (!DoesSoundExist(sound)) { return; }
        sound.source.Play();
    }

    public void Stop(String name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (!DoesSoundExist(sound)) { return; }
        sound.source.Stop();
    }

    public void SetPitch(string name, float newPitch)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (!DoesSoundExist(sound)) { return; }
        sound.source.pitch = newPitch;
    }

    private bool DoesSoundExist(Sound sound)
    {
        if (sound == null) 
        { 
            Debug.LogWarning("Sound: " + name + " not found!");
            return false; 
        }
        return true;
    }
}
