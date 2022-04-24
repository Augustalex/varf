using System;
using UnityEngine;

namespace Computer.Screens
{
    public class ScreenRoot : MonoBehaviour
    {
        public event Action<ScreenRoot> ChangedScreen;

        public void ChangeScreen(ScreenRoot newScreen)
        {
            ChangedScreen?.Invoke(newScreen);
        }
    }
}