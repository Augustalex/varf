using System.Linq;
using UnityEngine;

namespace Computer.Screens
{
    public class ScreenCanvas : MonoBehaviour
    {
        public ScreenRoot activeScreen;
        private GameObject[] _screens;

        void Start()
        {
            var screens = GetComponentsInChildren<ScreenRoot>();
            _screens = screens.Select(s => s.gameObject).ToArray();
            foreach (var screenRoot in screens)
            {
                screenRoot.ChangedScreen += OnChangedScreen;
            }
            
            UpdateActiveScreen();
        }

        private void OnChangedScreen(ScreenRoot newScreen)
        {
            activeScreen = newScreen;
            UpdateActiveScreen();
        }

        private void UpdateActiveScreen()
        {
            foreach (var screen in _screens)
            {
                screen.SetActive(screen == activeScreen.gameObject);
            }
        }
    }
}