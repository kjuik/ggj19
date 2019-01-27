using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class Person
    {
        public string Id;
        public string Name;
        public string Bio;
        public string FurnitureComment;
        public string TheftComment;
        public PersonMetaData MetaData;
        public List<DialogueBlock> Dialogue = new List<DialogueBlock>();

        public PersonStatus Status { get; set; } = PersonStatus.Stranger;
        
        public Person(XElement personElement, Dictionary<string, PersonMetaData> personMetaDataById)
        {
            Id = personElement.GetChildValue("id");
            Name = personElement.GetChildValue("name");
            Bio = personElement.GetChildValue("bio");
            FurnitureComment = personElement.GetChildValue("furnitureComment");
            TheftComment = personElement.GetChildValue("theftComment");

            if (!personMetaDataById.TryGetValue(Id, out MetaData))
            {
                Debug.LogError("There is no meta data for ID: " + Id);
                MetaData = new PersonMetaData();
            }
            
            foreach (var element in personElement.Element(XName.Get("dialogue")).Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "they":
                    case "you":
                        Dialogue.Add(DialogueLine.Parse(element));
                        break;
                    
                    case "question":
                        Dialogue.Add(DialogueQuestion.Parse(element));
                        break;
                        
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}