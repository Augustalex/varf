using System;
using UnityEngine;
using UnityEngine.UI;

public class Person
{
    public Texture Headshot;
    public string FullName;
    public System.Guid UniqueId;
    private bool _dead;

    public event Action Killed;
    public event Action WasHurt;

    public bool CanKill()
    {
        return !_dead;
    }

    public void Kill()
    {
        _dead = true;
        Killed?.Invoke();
    }

    public void Hurt()
    {
        WasHurt?.Invoke();
    }
}