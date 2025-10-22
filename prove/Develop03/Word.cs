namespace ScriptureMemorizer
{
    // Represents a single word with its text and visibility state.
    class Word
    {
        private readonly string text; // The word text
        private bool isHidden;        // Whether the word is hidden

        public Word(string text)
        {
            this.text = text;
            this.isHidden = false;
        }

        // Hide this word
        public void Hide() => isHidden = true;

        // Unhide this word (make visible)
        public void Unhide() => isHidden = false;

        // Check if the word is hidden
        public bool IsHidden() => isHidden;

        // Get displayed text: underscores if hidden, else real word
        public string GetDisplayText() =>
            isHidden ? new string('_', text.Length) : text;

        // Get original text
        public string GetText() => text;
    }
}
