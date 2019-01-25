using System.Xml.Linq;

namespace Data
{
    public class DialogueQuestion : DialogueBlock
    {
        public DialogueBlockType Type => DialogueBlockType.Question;
        
        public DialogueLine Question;
        public DialogueAnswer[] Answers;
        public DialogueLine FailLine;
        
        public static DialogueQuestion Parse(XElement element)
        {
            return new DialogueQuestion();
        }
    }
}