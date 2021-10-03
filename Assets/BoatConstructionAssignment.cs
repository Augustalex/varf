using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatConstructionAssignment : Paper
{
    public int count;
    private WorkerDaysCountOnReport _workerDaysCount;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _workerDaysCount = GetComponentInChildren<WorkerDaysCountOnReport>();

        _workerDaysCount.Set(count);
    }
    
    public override void Enact()
    {
        _gameManager.StartBoatConstructionAssignment(count);
    }
}
