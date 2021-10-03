using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerDaysCount : MonoBehaviour
{
    private TMP_Text _text;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _text.text = $"{_gameManager.GetBoatProgressionPercentage()}%";
    }
}
