using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Computer.Screens;
using Space;
using UnityEngine;

public class JournalSystem : MonoBehaviour
{
    private int _index = 0;
    private GameObject[] _journals;
    private ScreenRoot _screenRoot;
    private bool _changeOnNextTick;

    void Awake()
    {
        _screenRoot = GetComponent<ScreenRoot>();
        _journals = transform.parent
            .GetComponentsInChildren<SimpleTextScreen>()
            .Select(o => o.gameObject)
            .ToArray();
    }

    private void OnEnable()
    {
        _changeOnNextTick = true;
    }

    private void Update()
    {
        if (_changeOnNextTick)
        {
            _changeOnNextTick = false;
            var journal = _journals[_index];
            _screenRoot.ChangeScreen(journal.GetComponent<ScreenRoot>());
        }
    }

    public void GoToNextEntry()
    {
        _index += 1;

        var journalEvent = _journals[_index].GetComponent<JournalEvent>();
        if (journalEvent)
        {
            journalEvent.Consume();
        }
    }
}