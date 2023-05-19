using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Audio;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioMixerGroup audioMixer;
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
            sound.source.outputAudioMixerGroup = audioMixer;

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

    public void Stop(String name, float fadeDuration = 0f)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (!DoesSoundExist(sound)) { return; }
        float original = sound.source.volume;
        sound.source.DOFade(0f, fadeDuration);
        StartCoroutine(StopSound(sound, fadeDuration, original));
    }

    public bool IsPlaying(String name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (!DoesSoundExist(sound)) { return false; }
        return sound.source.isPlaying;
    }

    public void SetPitch(string name, float newPitch)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (!DoesSoundExist(sound)) { return; }
        sound.source.pitch = newPitch;
    }

    public void SetVolume(string name, float newVolume)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (!DoesSoundExist(sound)) { return; }
        sound.source.volume = newVolume;
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

    public void PauseSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.Pause();
        }
    }

    public void ResumeSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.UnPause();
        }
    }

    IEnumerator StopSound(Sound sound, float after, float original)
    {
        yield return new WaitForSeconds(after);

        sound.source.Stop();
        sound.source.volume = original;
    }
}
