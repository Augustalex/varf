using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoatConstructionAssignment : Paper
{
    public int count;
    private WorkerDaysCountOnReport _workerDaysCount;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _workerDaysCount = GetComponentInChildren<WorkerDaysCountOnReport>();

        var actualCount = Random.Range(1000, 10000);
        count = actualCount;
        _workerDaysCount.Set(actualCount);
    }

    public override void Enact()
    {
        _gameManager.StartBoatConstructionAssignment(count);
    }
}