using System;
using Space;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Computer.Screens
{
    [Serializable]
    public struct NavigationItem
    {
        public string text;
        public ScreenRoot destination;
    }

    public class ScreenController : MonoBehaviour
    {
        public event Action Previous;
        public event Action Next;
        public event Action OK;

        private void Awake()
        {
            var inputReceiver = GetComponent<PlayerInputReceiver>();
            inputReceiver.OnMove += OnMove;
            inputReceiver.OnSelect += OnSelect;
        }

        public enum KeyActionTypes
        {
            Previous,
            OK,
            Next
        }

        public void Press(KeyActionTypes keyActionType)
        {
            switch (keyActionType)
            {
                case KeyActionTypes.Next:
                    PressNext();
                    break;
                case KeyActionTypes.Previous:
                    PressPrevious();
                    break;
                case KeyActionTypes.OK:
                    PressOK();
                    break;
            }
        }

        public void PressPrevious()
        {
            Previous?.Invoke();
        }

        public void PressNext()
        {
            Next?.Invoke();
        }

        public void PressOK()
        {
            OK?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            var move = callbackContext.ReadValue<Vector2>();
            if (move.y > .8f)
            {
                PressPrevious();
            }
            else if (move.y < -.8f)
            {
                PressNext();
            }
        }

        public void OnSelect(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                PressOK();
            }
        }
    }
}