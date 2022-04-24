using System.Collections;
using System.Collections.Generic;
using Computer.Screens;
using Space;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputMediator : MonoBehaviour
{
    public PlayerInputReceiver receiver;

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        receiver.Move(callbackContext);
    }

    public void OnSelect(InputAction.CallbackContext callbackContext)
    {
        receiver.Select(callbackContext);
    }
}