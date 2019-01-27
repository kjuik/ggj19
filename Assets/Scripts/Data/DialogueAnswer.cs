using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DefaultNamespace;

namespace Data
{
    public class DialogueAnswer
    {
        public bool Correct { get; }
        public string YourAnswerText { get; }
        public bool ReactionLinesOngoing => reactionLineIndex < theirReactionLines.Count;

        int reactionLineIndex;
        readonly List<DialogueLine> theirReactionLines;

        public DialogueAnswer(bool correct, string yourAnswerText, List<DialogueLine> theirReactionLines)
        {
            Correct = correct;
            YourAnswerText = yourAnswerText;
            this.theirReactionLines = theirReactionLines;
        }

        public static DialogueAnswer Parse(XElement element) {
            var responseElements = element.Elements().ToList();
            
            return new DialogueAnswer(
                element.Name.LocalName.Equals("answerRight"),
                element.GetChildValue("you"),
                responseElements.Select(DialogueLine.Parse).ToList().GetRange(1, responseElements.Count - 1)
            );
        }
        
        public DialogueLine NextReactionLine() {
            var res = theirReactionLines[reactionLineIndex];
            reactionLineIndex++;
            return res;
        }
    }
}