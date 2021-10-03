using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperGenerator : MonoBehaviour
{
    public GameObject[] paperTemplates;
    public Transform spawnPoint;
    private float _cooldown;
    private bool _paused;

    void Update()
    {
        if (_paused) return;
        
        _cooldown -= Time.deltaTime;
        
        if (_cooldown < 0)
        {
            _cooldown = Random.Range(5, 10);
            GeneratePaper();
        }
    }

    private void GeneratePaper()
    {
        var paperTemplate = paperTemplates[Random.Range(0, paperTemplates.Length)];
        var paper = Instantiate(paperTemplate);
        
        paper.transform.position = spawnPoint.position;
        paper.transform.rotation = Quaternion.Euler(
            0, Random.Range(230, 320), 0
        );
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }
}