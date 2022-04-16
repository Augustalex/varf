using System;
using System.Collections;
using System.Collections.Generic;
using Computer.Screens;
using UnityEngine;

public class KeyboardButton : MonoBehaviour
{
    public ScreenController.KeyActionTypes keyActionType;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private ScreenController _screenController;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _screenController = FindObjectOfType<ScreenController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<ModernArmController>())
        {
            other.rigidbody.AddForce(Vector3.up * 50f, ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.down * 20f, ForceMode.Impulse);
            _audioSource.Play();

            _screenController.Press(keyActionType);
        }
    }
}