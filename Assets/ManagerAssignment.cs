using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ManagerAssignment : Paper
{
    public GameObject bloodDecalTemplate;

    public RawImage headshotContainer;
    public TMP_Text nameDisplay;
    private Person _person;

    private void Start()
    {
        _person = PeopleBase.Get().CreatePerson();
        nameDisplay.text = _person.FullName;
        headshotContainer.texture = _person.Headshot;

        _person.Killed += OnKilled;
        _person.WasHurt += OnWasHurt;
    }
    
    private void OnWasHurt()
    {
        StampInBlood();
    }

    private void OnKilled()
    {
        for (int i = 0; i < Random.Range(2,4); i++)
        {
            StampInBlood();
        }
    }

    private void StampInBlood()
    {
        var decal = Instantiate(bloodDecalTemplate, transform);
        decal.transform.position = new Vector3(
            transform.position.x + (transform.localScale.x * (Random.value * 2f - 1f)),
            transform.position.y + .1f,
            transform.position.z + (transform.localScale.z * (Random.value * 2f - 1f))
        );
        decal.transform.rotation = Quaternion.Euler(
            0,
            Random.value * 360,
            0
        );
    }

    public override void Enact()
    {
        FindObjectOfType<GameManager>().AddWorkers(100);
    }
}