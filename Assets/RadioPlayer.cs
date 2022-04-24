using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RadioPlayer : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource _audioSource;

    public AudioSource hitSource;
    private float _hitCooldownUntil;
    private static RadioPlayer _instance;

    public static RadioPlayer Get()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayRandomSong();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Time.time < _hitCooldownUntil) return;

        if (other.rigidbody && other.rigidbody.velocity.magnitude > 2f)
        {
            _hitCooldownUntil = Time.time + 1f;
            PlayRandomSong();
            hitSource.Play();
        }
    }

    public void PlayRandomSong()
    {
        _audioSource.Stop();
        _audioSource.clip = songs[Random.Range(0, songs.Length)];
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}