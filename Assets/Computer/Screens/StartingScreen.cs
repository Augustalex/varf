using System;
using UnityEngine;

namespace Computer.Screens
{
    public class StartingScreen : MonoBehaviour
    {
        public ScreenRoot mainScreen;
        private ScreenController _screenController;
        private ScreenRoot _screenRoot;

        void Awake()
        {
            _screenRoot = GetComponent<ScreenRoot>();
            _screenController = GetComponentInParent<ScreenController>();

            _screenController.Next += OnNext;
            _screenController.Previous += OnPrevious;
            _screenController.OK += OnOK;
        }

        private void OnOK()
        {
            Debug.Log("CHANGE SCREEN");
            _screenRoot.ChangeScreen(mainScreen);
        }

        private void OnPrevious()
        {
        }

        private void OnNext()
        {
        }
    }
}