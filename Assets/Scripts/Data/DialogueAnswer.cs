using System.Xml.Linq;
using DefaultNamespace;

namespace Data
{
    public class DialogueAnswer
    {
        public bool Correct { get; }
        public string YourAnswerText { get; }
        public DialogueLine TheirReactionLine { get; }

        public DialogueAnswer(bool correct, string yourAnswerText, DialogueLine theirReactionLine)
        {
            Correct = correct;
            YourAnswerText = yourAnswerText;
            TheirReactionLine = theirReactionLine;
        }

        public static DialogueAnswer Parse(XElement element)
        {
            return new DialogueAnswer(
                element.Name.LocalName.Equals("answerRight"),
                element.GetChildValue("you"),
                DialogueLine.Parse(element.Element("they"))
            );
        }
    }
}