using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModernArmController : MonoBehaviour
{
    public GameObject hand;
    public GameObject pointer;
    public GameObject possiblePointer;
    private Rigidbody _rigidbody;
    private Vector2 _move;
    private Vector3 _startingPosition;
    private bool _center;
    private bool _shift;
    private float _cooldownUntil;

    public event Action Grabbed;
    public event Action Hovering;
    public event Action Dropped;

    void Start()
    {
        _startingPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnMoveInput(InputAction.CallbackContext callbackContext)
    {
        _move = callbackContext.ReadValue<Vector2>();
    }

    public void OnReset(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            _center = true;
        }
        else if (callbackContext.canceled)
        {
            _center = false;
        }
    }

    public void OnShift(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            _shift = true;
        }
        else if (callbackContext.canceled)
        {
            // var currentPosition = _rigidbody.position;
            // transform.position = new Vector3(currentPosition.x, _startingPosition.y, currentPosition.z);
            // Debug.Log("CURRENT:  " + currentPosition + ", rbpos: " + _rigidbody.position);
            _shift = false;
        }
    }

    void Update()
    {
        if (_center)
        {
            transform.position = _startingPosition;
        }
        else
        {
            if (_shift)
            {
                _cooldownUntil = Time.time + .5f;
                _rigidbody.AddForce(Vector3.down * 40000f * Time.deltaTime, ForceMode.Acceleration);

                if (Time.time < _cooldownUntil)
                {
                }
                else
                {
                    _cooldownUntil = Time.time + .5f;
                    _rigidbody.AddForce(Vector3.down * 80f, ForceMode.Impulse);
                }
            }
            else
            {
                var moveVector = new Vector3(-_move.y, 0, _move.x);
                _rigidbody.AddForce(moveVector * 30000f * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        var currentPosition = _rigidbody.position;
        var target = new Vector3(currentPosition.x, _startingPosition.y, currentPosition.z);
        if (_rigidbody.velocity.y < 1f && (target - currentPosition).magnitude > .25f)
        {
            _rigidbody.AddForce((target - currentPosition).normalized * 5000f * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log("UP!");
        // var direction = -(_rigidbody.velocity.normalized);
        // var adjustedDirection = new Vector3(
        //     direction.x * .5f,
        //     direction.y,
        //     direction.z * .1f
        // );
        // _rigidbody.AddForce(adjustedDirection * 50f, ForceMode.Impulse);
    }

    //
    // [DllImport("user32.dll")]
    // static extern bool SetCursorPos(int X, int Y);
}