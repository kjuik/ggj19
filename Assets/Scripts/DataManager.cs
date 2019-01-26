using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Data;
using UnityEngine;
using UnityUtilities;

public class DataManager : PersistentSingletonMonoBehaviour<DataManager>
{
    [SerializeField] TextAsset peopleXML;
    [SerializeField] PersonMetaData[] personMetaDataList;
    
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

        ChosenPerson = People[0];
    }

    public void ChooseNextPerson() =>
        ChosenPerson = People[(People.IndexOf(ChosenPerson) + 1) % (People.Count)];
}
