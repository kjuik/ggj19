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
        public DialogueLine FailLine;

        public DialogueQuestion(DialogueLine questionLine, List<DialogueAnswer> answers, DialogueLine failLine)
        {
            QuestionLine = questionLine;
            Answers = answers;
            FailLine = failLine;
        }

        public static DialogueQuestion Parse(XElement element)
        {
            var questionLine = new DialogueLine(Speaker.They, element.GetChildValue("text"));
            var answers = new List<DialogueAnswer>();
            foreach (var subElement in element.Elements())
            {
                AddAnswers(answers, subElement, "answerRight", true);
                AddAnswers(answers, subElement, "answerWrong", false);
            }

            var failLine = new DialogueLine(Speaker.They, element.GetChildValue("failure"));
            return new DialogueQuestion(questionLine, answers, failLine);
        }

        static void AddAnswers(List<DialogueAnswer> answers, XElement subElement, string desiredElementName, bool isRightAnswer)
        {
            if (subElement.Name.LocalName.Equals(desiredElementName))
            {
                answers.Add(new DialogueAnswer(isRightAnswer, subElement.Value));
            }
        }
    }
}