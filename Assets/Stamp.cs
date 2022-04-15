using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public AudioClip effect;
    private AudioPlayer _source;

    void Start()
    {
        _source = FindObjectOfType<AudioPlayer>();
    }
    public void StampSound()
    {
        _source.Play(effect, .3f);
    }
}
