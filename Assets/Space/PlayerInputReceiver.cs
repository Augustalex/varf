using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Space
{
    public class PlayerInputReceiver : MonoBehaviour
    {
        public event Action<InputAction.CallbackContext> OnMove;
        public event Action<InputAction.CallbackContext> OnSelect;

        public void Move(InputAction.CallbackContext callbackContext)
        {
            OnMove?.Invoke(callbackContext);
        }

        public void Select(InputAction.CallbackContext callbackContext)
        {
            OnSelect?.Invoke(callbackContext);
        }
    }
}