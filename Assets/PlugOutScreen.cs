using System.Collections;
using System.Collections.Generic;
using Computer.Screens;
using TMPro;
using UnityEngine;

public class PlugOutScreen : MonoBehaviour
{
    private TMP_Text _text;
    private float _rate = 1f;
    private float _time;
    private ScreenController _screenController;
    private ScreenRoot _screenRoot;

    public ScreenRoot nextScreen;
    private bool _canWakeUp;
    public MainScreen dailyMotivationScreen;

    void Awake()
    {
        _screenRoot = GetComponent<ScreenRoot>();
        _screenController = GetComponentInParent<ScreenController>();
    }

    void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        RadioPlayer.Get().Stop();
        _time = 0;
        _rate = 50f;
        _screenController.OK += OnOK;
    }

    private void OnDisable()
    {
        _screenController.OK -= OnOK;
    }

    void Update()
    {
        var hourText = Mathf.FloorToInt(_time / 60);
        var minutesText = Mathf.FloorToInt(_time % 60);

        if (hourText >= 12)
        {
            _canWakeUp = true;
            if (Mathf.FloorToInt(Time.time) % 2 == 0)
            {
                _text.text = "WAKE UP [1200 hours]";
            }
            else
            {
                _text.text = "";
            }
        }
        else
        {
            var timeText = $"{hourText}00 hours";
            _text.text = $"sleep mode [{timeText}]";

            _time += _rate * Time.deltaTime;
            _rate *= 1 + .2f * Time.deltaTime;
        }
    }

    private void OnOK()
    {
        if (!_canWakeUp) return;
        _canWakeUp = false;

        RadioPlayer.Get().PlayRandomSong();

        var journalSystem = nextScreen.GetComponent<JournalSystem>();
        if (journalSystem)
        {
            journalSystem.GoToNextEntry();
        }

        dailyMotivationScreen.GenerateNewText();

        _screenRoot.ChangeScreen(nextScreen);
    }
}