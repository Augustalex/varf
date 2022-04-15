using System;
using System.Collections;
using System.Collections.Generic;
using BoatConstruction;
using JetBrains.Annotations;
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
    private bool _paused;

    private float _officeScore = 5;
    private static GameManager _instance;
    private ConstructionManager _constructionManager;

    public static GameManager Get()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
        _constructionManager = new ConstructionManager();

        _constructionManager.JobCompleted += OnJobCompleted;
    }

    void Start()
    {
        templateBank = GetComponent<TemplateBank>();
        officePointer = GetComponent<OfficePointer>();
        pointer = GetComponent<Pointer>();
        _reporter = Reporter.Get();

        GoToPlanningPhase();
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }

    private void Update()
    {
        if (_paused) return;

        if (_dayCooldown < 0)
        {
            _dayCooldown = 1;

            OnDayPassed();
        }

        _dayCooldown -= Time.deltaTime;
    }

    private void OnDayPassed()
    {
        _constructionManager.DoDailyWork(_workers);

        if (_workers > 0)
        {
            if (Random.value < .01)
            {
                _workers -= 1;
                _reporter.RegisterDeath(1);
            }

            if (Random.value < .1)
            {
                _reporter.ReportAccident(1);
            }
        }
    }

    private void OnJobCompleted(ConstructionJob job)
    {
        _reporter.BoatConstructed();
    }

    public void StartBoatConstructionAssignment(int workerDays)
    {
        _constructionManager.AddJob(workerDays);
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

    public float GetOfficeScore()
    {
        return _officeScore;
    }

    public void SetOfficeScore(float score)
    {
        _officeScore = score;
    }

    public ConstructionManager GetConstructionManager()
    {
        return _constructionManager;
    }
}