using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private WorkerMoraleScore _officeScore;

    private static int _reportIndex = 1;
    private ReportHeader _reportHeader;

    void Start()
    {
        _paperGenerator = FindObjectOfType<PaperGenerator>();
        _gameManager = FindObjectOfType<GameManager>();
        _reporter = Reporter.Get();

        _reportHeader = GetComponentInChildren<ReportHeader>();
        _hiredCount = GetComponentInChildren<HiredCount>();
        _deathCount = GetComponentInChildren<DeathCount>();
        _accidentCount = GetComponentInChildren<AccidentCount>();
        _boatConstructedCount = GetComponentInChildren<BoatConstructionCount>();
        _officeScore = GetComponentInChildren<WorkerMoraleScore>();

        _paperGenerator.Pause();
        _gameManager.Pause();
        _reporter.Pause();

        float newOfficeScore = _gameManager.GetOfficeScore();

        if (_reporter.dead + _reporter.accidents > (_reporter.hired / 20))
        {
            newOfficeScore -= 1;
        }

        if (_reporter.boatsConstructed > 1)
        {
            newOfficeScore += .5f;
        }

        _reportHeader.GetComponent<TMP_Text>().text = "REPORT #" + _reportIndex;
        _reportIndex += 1;

        _gameManager.SetOfficeScore(newOfficeScore);

        _hiredCount.Set(_reporter.hired);
        _deathCount.Set(_reporter.dead);
        _accidentCount.Set(_reporter.accidents);
        _boatConstructedCount.Set(_reporter.boatsConstructed);
        _officeScore.Set(newOfficeScore);

        _reporter.Clean();
    }

    void Update()
    {
    }

    public override void Enact()
    {
        _paperGenerator.Unpause();
        _gameManager.Unpause();
        _reporter.Unpause();
    }
}