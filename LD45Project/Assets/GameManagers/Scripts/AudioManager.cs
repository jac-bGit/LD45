using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    //sounds
    //public SoundGrops[] soundsGroups;
    public Sound[] soundsToSet;
    public static Sound[] sounds;

    void Start()
    {
        sounds = soundsToSet;
        CreateSources();
    }

    public void CreateSources()
    {
        foreach (Sound s in sounds)
        {
            Debug.Log("sounds[0].name: " + sounds[0].name);
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.loop = s.loop;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }
    }

    public static void PlaySound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
            s.audioSource.Play();
        else
            Debug.LogError("Sound  '" + soundName + "' doesn't exist!");
    }
}

//audio sound class
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource audioSource;

    public bool loop;
    [Range(0, 1f)]
    public float volume = 1;
    [Range(1, 3f)]
    public float pitch = 1;
}

