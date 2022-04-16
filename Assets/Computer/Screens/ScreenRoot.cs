using System;
using System.Collections;
using System.Collections.Generic;
using Computer.Screens;
using UnityEngine;

public class ScreenRoot : MonoBehaviour
{
    public event Action<ScreenRoot> ChangedScreen;

    private ScreenController _screenController;

    public void ChangeScreen(ScreenRoot newScreen)
    {
        ChangedScreen?.Invoke(newScreen);
    }
}