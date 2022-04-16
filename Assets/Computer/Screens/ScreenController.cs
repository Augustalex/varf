using System;
using UnityEngine;

namespace Computer.Screens
{
    public class ScreenController : MonoBehaviour
    {
        public event Action Previous;
        public event Action Next;
        public event Action OK;

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
    }
}