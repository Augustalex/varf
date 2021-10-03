using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentReport : Paper
{
    private HiredCount _hiredCount;
    private DeathCount _deathCount;
    private Reporter _reporter;
    private AccidentCount _accidentCount;
    private PaperGenerator _paperGenerator;
    private GameManager _gameManager;
    private BoatConstructionCount _boatConstructedCount;

    void Start()
    {
        _paperGenerator = FindObjectOfType<PaperGenerator>();
        _gameManager = FindObjectOfType<GameManager>();
        _reporter = Reporter.Get();
        
        _hiredCount = GetComponentInChildren<HiredCount>();
        _deathCount = GetComponentInChildren<DeathCount>();
        _accidentCount = GetComponentInChildren<AccidentCount>();
        _boatConstructedCount = GetComponentInChildren<BoatConstructionCount>();

        _paperGenerator.Pause();
        _gameManager.Pause();

        _hiredCount.Set(_reporter.hired);
        _deathCount.Set(_reporter.dead);
        _accidentCount.Set(_reporter.accidents);
        _boatConstructedCount.Set(_reporter.boatsConstructed);
        
        _reporter.Clean();
    }

    void Update()
    {
        
    }

    public override void Enact()
    {
        _paperGenerator.Unpause();
        _gameManager.Unpause();
    }
}
