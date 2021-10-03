using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TemplateBank))]
[RequireComponent(typeof(OfficePointer))]
public class GameManager : MonoBehaviour
{
    public enum GamePhase
    {
        Planning,
        Construction,
        Simulation,
        Report
    }

    public GamePhase currentPhase = GamePhase.Planning;

    [NonSerialized] public TemplateBank templateBank;

    [NonSerialized] public OfficePointer officePointer;

    [NonSerialized] public Pointer pointer;

    private int _workers;
    private Reporter _reporter;
    private double _dayCooldown;
    private int _boatProgress;
    private bool _boatConstructionInProgress;
    private int _boatAssignmentWorkerDays;

    void Start()
    {
        templateBank = GetComponent<TemplateBank>();
        officePointer = GetComponent<OfficePointer>();
        pointer = GetComponent<Pointer>();
        _reporter = Reporter.Get();

        GoToPlanningPhase();
    }

    private void Update()
    {
        if (_workers > 0)
        {
            if (Random.value < .001)
            {
                _workers -= 1;
                _reporter.RegisterDeath(1);
            }

            if (Random.value < .01)
            {
                _reporter.ReportAccident(1);
            }
        }
        
        if (_dayCooldown < 0)
        {
            _dayCooldown = 1;

            if (_boatConstructionInProgress)
            {
                _boatProgress += _workers;

                if (_boatProgress > _boatAssignmentWorkerDays)
                {
                    _boatConstructionInProgress = false;
                    _reporter.BoatConstructed();
                }
            }
        }

        _dayCooldown -= Time.deltaTime;
    }

    public void StartBoatConstructionAssignment(int workerDays)
    {
        _boatProgress = 0;
        _boatAssignmentWorkerDays = workerDays;
        _boatConstructionInProgress = true;
    }

    public void GoToPlanningPhase()
    {
        currentPhase = GamePhase.Planning;

        officePointer.ReloadCamera();
    }

    public void AddWorkers(int workers)
    {
        _workers += workers;
        _reporter.Hire(workers);
    }

    public int GetWorkerCount()
    {
        return _workers;
    }

    public int GetBoatProgressionPercentage()
    {
        if (_boatConstructionInProgress)
        {
            var boatProgress = (double) _boatProgress;
            var assignmentTotal = (double) _boatAssignmentWorkerDays;
            double percentageFactor = boatProgress / assignmentTotal;
            Debug.Log($"{percentageFactor} - {boatProgress} - {assignmentTotal}");
            return (int) Math.Ceiling(percentageFactor * 100);
        }
        else
        {
            return 0;
        }
    }

    public bool HasBoatConstructionInProgress()
    {
        return _boatConstructionInProgress;
    }
}