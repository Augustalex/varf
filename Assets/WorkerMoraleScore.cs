using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerMoraleScore : MonoBehaviour
{
    public string prefix;
    private TMP_Text _text;
    private int _count;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _text.text = $"{prefix}{_count}/10";
    }

    public void Set(int count)
    {
        _count = count;
    }
}
