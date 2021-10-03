using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Reporter : MonoBehaviour
{
    public GameObject reportTemplate;
    public Transform spawnPoint;
    
    [NonSerialized]
    public int hired;
    
    [NonSerialized]
    public int dead;
    
    [NonSerialized]
    public int accidents;
    
    [NonSerialized]
    public int boatsConstructed;
    
    private static Reporter _instance;
    private float _cooldown = 30;
    private bool _paused;

    public static Reporter Get()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }

    void Update()
    {
        if (_paused) return;
        
        _cooldown -= Time.deltaTime;

        if (_cooldown < 0)
        {
            _cooldown = 30;

            GenerateReport();
        }
    }

    private void GenerateReport()
    {
        var report= Instantiate(reportTemplate);
            
        report.transform.position = spawnPoint.position;
        report.transform.rotation = Quaternion.Euler(
            0, 285, 0
        );
    }

    public void Hire(int count)
    {
        hired += count;
    }

    public void RegisterDeath(int count)
    {
        dead += count;
    }

    public void BoatConstructed()
    {
        boatsConstructed += 1;
    }

    public void Clean()
    {
        hired = 0;
        dead = 0;
        accidents = 0;
        boatsConstructed = 0;
    }

    public void ReportAccident(int count)
    {
        accidents = count;
    }
}
