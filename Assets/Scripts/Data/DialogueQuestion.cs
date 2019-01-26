using System.Collections.Generic;
using System.Xml.Linq;
using DefaultNamespace;

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
            var questionLine = new DialogueLine(Speaker.They, element.GetChildValue("they"));
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