using System;
using System.Collections;
using System.Collections.Generic;
using Space;
using UnityEngine;

public class FoundItemEvent : MonoBehaviour
{
    public PlayerItem item;

    private bool _consumed = false;

    void Start()
    {
        GetComponent<JournalEvent>().Consumed += Consume;
    }

    private void Consume()
    {
        if (_consumed) return;

        FindObjectOfType<Player>().AddItem(item);
        _consumed = true;
    }
}