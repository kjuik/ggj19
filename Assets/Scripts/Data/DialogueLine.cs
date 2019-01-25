using System.Xml.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Data
{
    public class DialogueLine : DialogueBlock
    {
        public DialogueBlockType Type => DialogueBlockType.Line;

        public Speaker Speaker;
        public string Text;

        public DialogueLine(Speaker speaker, string text)
        {
            Speaker = speaker;
            Text = text;
        }

        public static DialogueLine Parse(XElement element)
        {
            return new DialogueLine(
                element.Name.LocalName == "they" ? Speaker.They : Speaker.You,
                element.Value
                );
        }
    }
}