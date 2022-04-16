using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardButton : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<ModernArmController>())
        {
        Debug.Log("COLLISION BUTTON!");
            other.rigidbody.AddForce(Vector3.up * 50f, ForceMode.Impulse);
            _rigidbody.AddForce(Vector3.down * 20f, ForceMode.Impulse);
            _audioSource.Play();
        }
    }
}