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
    
    void Awake()
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
    }
}
