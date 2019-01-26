namespace Data
{
    public class DialogueAnswer
    {
        public bool Correct;
        public string Text;

        public DialogueAnswer(bool correct, string text)
        {
            Correct = correct;
            Text = text;
        }
    }
}