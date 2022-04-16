using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Computer.Screens
{
    public class MainScreen : MonoBehaviour
    {
        private ScreenController _screenController;
        private TMP_Text _text;

        public string[] texts;
        private float _cooldownUntil;

        void Awake()
        {
            _screenController = GetComponentInParent<ScreenController>();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            _screenController.Next += OnNext;
            _screenController.Previous += OnPrevious;
            _screenController.OK += OnOK;
        }

        private void OnDisable()
        {
            _screenController.Next -= OnNext;
            _screenController.Previous -= OnPrevious;
            _screenController.OK -= OnOK;
        }

        protected virtual void OnOK()
        {
            if (Time.time < _cooldownUntil) return;
            _cooldownUntil = Time.time + .1f;

            _text.text = texts[Random.Range(0, texts.Length)];
        }

        protected virtual void OnPrevious()
        {
            if (Time.time < _cooldownUntil) return;
            _cooldownUntil = Time.time + .1f;

            _text.text = texts[Random.Range(0, texts.Length)];
        }

        protected virtual void OnNext()
        {
            if (Time.time < _cooldownUntil) return;
            _cooldownUntil = Time.time + .1f;

            _text.text = texts[Random.Range(0, texts.Length)];
        }
    }
}