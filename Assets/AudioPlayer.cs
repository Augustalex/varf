using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip, float f)
    {
        _source.PlayOneShot(clip);
    }
}