using System.Collections.Generic;
using System.Xml.Linq;
using Data;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] TextAsset peopleXML;

    [SerializeField] List<Person> people = new List<Person>();
    
    void Awake()
    {
        var peopleRoot = XElement.Parse(peopleXML.text);
        foreach (var personElement in peopleRoot.Elements())
        {
            people.Add(new Person(personElement));
        }
    }
}
