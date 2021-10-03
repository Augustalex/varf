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
}