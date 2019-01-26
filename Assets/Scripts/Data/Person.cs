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
        public List<DialogueBlock> Dialogue = new List<DialogueBlock>();
        
        public Person(XElement personElement)
        {
            Id = personElement.GetChildValue("id");
            Name = personElement.GetChildValue("name");
            Bio = personElement.GetChildValue("bio");
            FurnitureComment = personElement.GetChildValue("furnitureComment");

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