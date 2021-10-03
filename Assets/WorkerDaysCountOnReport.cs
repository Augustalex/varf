using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerDaysCountOnReport : MonoBehaviour
{
    private TMP_Text _text;
    private int _count;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _text.text = $"{_count}";
    }

    public void Set(int count)
    {
        _count = count;
    }
}
