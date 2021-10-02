using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject officeScene;
    public GameObject worldScene;
    
    private static GameSceneManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static GameSceneManager Get()
    {
        return _instance;
    }

    public void ShowOfficeScene()
    {
        worldScene.SetActive(false);
        officeScene.SetActive(true);
    }

    public void ShowWorldScene()
    {
        officeScene.SetActive(false);
        worldScene.SetActive(true);
    }
}
