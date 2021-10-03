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

    void Start()
    {
        templateBank = GetComponent<TemplateBank>();
        officePointer = GetComponent<OfficePointer>();
        pointer = GetComponent<Pointer>();

        GoToPlanningPhase();
    }

    public void GoToPlanningPhase()
    {
        currentPhase = GamePhase.Planning;

        officePointer.ReloadCamera();
    }
}