using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        instance = this;
        foreach (Sound sound in sounds)
        {
            sound.audSrc = gameObject.AddComponent<AudioSource>();
            sound.audSrc.clip = sound.clip;
            sound.audSrc.volume = sound.volume;
            sound.audSrc.loop = sound.isLoop;
        }
    }

    private void Start()
    {
        Play("BackGround");
    }


    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        s.audSrc.Play();
    }
}
