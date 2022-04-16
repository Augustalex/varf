using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private ArmController _armController;
    private Animator _animationController;
    private float _stopHovering;
    private bool _holding;

    void Start()
    {
        _animationController = GetComponentInChildren<Animator>();
        _armController = GetComponentInParent<ArmController>();

        _armController.Grabbed += OnGrabbed;
        _armController.Dropped += OnDropped;
        _armController.Hovering += OnHovering;
    }

    private void Update()
    {
        var hovering = Time.time < _stopHovering;
        if (_holding || hovering)
        {
            transform.localScale = new Vector3(1.05f, .5f, 1.05f);;
        }
        else
        {
            transform.localScale = new Vector3(.9f, .5f, .9f);;
        }
    }

    private void OnHovering()
    {
        _stopHovering = Time.time + .2f;
    }

    private void OnDropped()
    {
        _holding = false;
        _animationController.SetBool("Holding", false);
    }

    private void OnGrabbed()
    {
        _holding = true;
        _animationController.SetBool("Holding", true);
    }

    // void Update()
    // {
    //     if (_armController.pointer)
    //     {
    //         Hold();
    //     }
    //     else
    //     {
    //         Drop();
    //     }
    // }
    //
    // private void Hold()
    // {
    //     transform.rotation = Quaternion.Euler(-90, 0, 0);
    // }
    //
    // private void Drop()
    // {
    //     transform.rotation = Quaternion.Euler(0, 0, 0);
    // }
}