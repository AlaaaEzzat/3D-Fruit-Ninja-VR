using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    [Range(1, 3)]
    public float pitch;

    [Range(0, 1)]
    public float spatialBlend;

    public bool playOnAwake;

    public bool isLoop;

    [HideInInspector]
    public AudioSource audSrc;
}
