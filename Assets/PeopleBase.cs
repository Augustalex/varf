using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PeopleBase : MonoBehaviour
{
    public Texture[] headshots;

    private Dictionary<System.Guid, Person> people = new Dictionary<Guid, Person>();
    private Stack<Texture> _headshots;
    private static PeopleBase _instance;

    public static PeopleBase Get()
    {
        return _instance;
    }

    private void Awake()
    {
        _headshots = new Stack<Texture>(headshots);

        _instance = this;
    }

    public Person CreatePerson()
    {
        var headshot = _headshots.Pop();

        var newPerson = new Person()
        {
            FullName = NameGenerator.RandomName(),
            Headshot = headshot,
            UniqueId = System.Guid.NewGuid()
        };

        people[newPerson.UniqueId] = newPerson;

        return newPerson;
    }

    public Person GetKillablePerson()
    {
        return people.Values.OrderBy(v => Random.value).First(v => v.CanKill());
    }
}