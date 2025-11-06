namespace ScriptureMemorizer
{
    // Represents a single word from a scripture (visible or hidden).
    class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
            _isHidden = false;
        }

        public void Hide() { _isHidden = true; }

        public void Unhide() { _isHidden = false; }

        public bool IsHidden() { return _isHidden; }

        public string GetDisplayText()
        {
            return _isHidden ? new string('_', _text.Length) : _text;
        }

        public string GetText() { return _text; }
    }
}
