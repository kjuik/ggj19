﻿using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Data;
using UnityEngine;
using UnityUtilities;

public class DataManager : PersistentSingletonMonoBehaviour<DataManager>
{
    [SerializeField] TextAsset peopleXML;
    [SerializeField] PersonMetaData[] personMetaDataList;
    [SerializeField] int defaultPerson = 0;
    
    public List<Person> People { get; private set; }
    public Person ChosenPerson { get; private set; }
    
    protected override void OnPersistentSingletonAwake()
    {
        var personMetaDataById = new Dictionary<string, PersonMetaData>();
        foreach (var personMetaData in personMetaDataList)
        {
            personMetaDataById[personMetaData.name] = personMetaData;
        }
        
        People = new List<Person>();        
        var peopleRoot = XElement.Parse(peopleXML.text);
        foreach (var personElement in peopleRoot.Elements())
        {
            People.Add(new Person(personElement, personMetaDataById));
        }

        ChosenPerson = People[defaultPerson];
    }

    public void ChooseNextAvailablePerson()
    {
        var availablePeople = People.Where(p => p.Status == PersonStatus.Available).ToList();
        ChosenPerson = availablePeople[(availablePeople.IndexOf(ChosenPerson) + 1) % (availablePeople.Count)];
    }

    public void Reset() {
        foreach (var person in People)
            person.Status = PersonStatus.Available;
        ChosenPerson = People[0];
    }
}
