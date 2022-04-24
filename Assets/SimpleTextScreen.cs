using System.Collections;
using System.Collections.Generic;
using Computer.Screens;
using TMPro;
using UnityEngine;

public class SimpleTextScreen : MonoBehaviour
{
    private ScreenController _screenController;

    public ScreenRoot whenDone;
    private TMP_Text _text;

    void Awake()
    {
        _screenController = GetComponentInParent<ScreenController>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _screenController.OK += OnOK;
    }

    private void OnDisable()
    {
        _screenController.OK -= OnOK;
    }

    private void OnOK()
    {
        Debug.Log("OK!");
        GetComponent<ScreenRoot>().ChangeScreen(whenDone);
    }
}