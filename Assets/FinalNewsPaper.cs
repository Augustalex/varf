using System;
using UnityEngine;

public class FinalNewsPaper : Paper
{
    private PaperGenerator _paperGenerator;
    private GameManager _gameManager;
    private Reporter _reporter;

    private void Start()
    {
        _paperGenerator = FindObjectOfType<PaperGenerator>();
        _gameManager = FindObjectOfType<GameManager>();
        _reporter = Reporter.Get();

        _paperGenerator.Pause();
        _gameManager.Pause();
        _reporter.Pause();
    }

    public override void Enact()
    {
        
    }
}