using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Data
{
    public class DialogueQuestion : DialogueBlock
    {
        public DialogueBlockType Type => DialogueBlockType.Question;
        
        public DialogueLine QuestionLine;
        public List<DialogueAnswer> Answers;

        public DialogueQuestion(DialogueLine questionLine, List<DialogueAnswer> answers)
        {
            QuestionLine = questionLine;
            Answers = answers;
        }

        public static DialogueQuestion Parse(XElement element)
        {
            var questionLine = DialogueLine.Parse(element.Element(XName.Get("they")));
            var answers = new List<DialogueAnswer>();
            foreach (var subElement in element.Elements())
            {
                var subElementName = subElement.Name.LocalName;
                if (subElementName.StartsWith("answer"))
                {
                    answers.Add(DialogueAnswer.Parse(subElement));
                }
            }

            return new DialogueQuestion(questionLine, answers);
        }
   }
}