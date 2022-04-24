using System.Collections.Generic;
using System.Linq;
using Computer.Screens;
using TMPro;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    private ScreenController _screenController;
    private TMP_Text _text;

    public NavigationItem[] items;

    private float _cooldownUntil;

    private string[] _parts;
    private int _selectorIndex = 0;

    private float InputCooldown = .15f;
    private ScreenRoot _screenRoot;

    void Awake()
    {
        _screenRoot = GetComponent<ScreenRoot>();
        _screenController = GetComponentInParent<ScreenController>();
        _text = GetComponentInChildren<TMP_Text>();

        ResetScreen();
    }

    private void ResetScreen()
    {
        // _parts = new[]
        // {
        //     "Daily motivational text",
        //     "Chat",
        //     "Job board",
        //     "Market",
        //     "Notes",
        //     "Plug out",
        // };

        GenerateText();
    }

    private void GenerateText()
    {
        _text.text = "OS 1.0 [warning unactivated copy detected]\n \n" + string.Join("\n", GetPartsWithSelector());
    }

    private IEnumerable<string> GetPartsWithSelector()
    {
        return items.Select((part, index) => $"{(_selectorIndex == index ? ">" : "")}{part.text}");
    }

    private void OnEnable()
    {
        _screenController.Next += OnNext;
        _screenController.Previous += OnPrevious;
        _screenController.OK += OnOK;
    }

    private void OnDisable()
    {
        Debug.Log("DISABLE");
        _screenController.Next -= OnNext;
        _screenController.Previous -= OnPrevious;
        _screenController.OK -= OnOK;
    }

    private void OnOK()
    {
        if (Time.time < _cooldownUntil) return;
        _cooldownUntil = Time.time + InputCooldown;

        if (_selectorIndex >= 0 && _selectorIndex < items.Length)
        {
            var selectedItem = items[_selectorIndex];
            _screenRoot.ChangeScreen(selectedItem.destination);
        }
    }

    private void OnPrevious()
    {
        IncrementSelector(-1);
    }

    private void OnNext()
    {
        IncrementSelector(1);
    }

    public void IncrementSelector(int increment)
    {
        if (Time.time < _cooldownUntil) return;
        _cooldownUntil = Time.time + InputCooldown;

        _selectorIndex += increment;
        ResetScreen();
    }
}