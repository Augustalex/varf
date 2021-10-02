using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ConstructionPhase))]
[RequireComponent(typeof(TemplateBank))]
[RequireComponent(typeof(Pointer))]
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
    [NonSerialized] public ConstructionPhase constructionPhase;

    [NonSerialized] public OfficePointer officePointer;

    [NonSerialized] public Pointer pointer;

    private GameSceneManager _gameSceneManager;
    
    void Start()
    {
        templateBank = GetComponent<TemplateBank>();
        constructionPhase = GetComponent<ConstructionPhase>();
        officePointer = GetComponent<OfficePointer>();
        pointer = GetComponent<Pointer>();

        _gameSceneManager = FindObjectOfType<GameSceneManager>();

        GoToPlanningPhase();
        
        DontDestroyOnLoad(gameObject);
    }

    public void GoToConstructionPhase()
    {
        currentPhase = GamePhase.Construction;

        _gameSceneManager.ShowWorldScene();

        pointer.ReloadCamera();

        var buildings = new List<GameObject>();
        var selection = new[]
        {
            templateBank.boatTemplate,
            templateBank.houseTemplate,
            templateBank.house90DegTemplate,
        };

        for (int i = 0; i < Random.Range(2, 5); i++)
        {
            buildings.Add(selection[Random.Range(0, selection.Length)]);
        }

        constructionPhase.gridObjects = buildings.ToArray();

        constructionPhase.StartPhase();
    }

    public void GoToPlanningPhase()
    {
        currentPhase = GamePhase.Planning;

        _gameSceneManager.ShowOfficeScene();

        officePointer.ReloadCamera();
    }
}